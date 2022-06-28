using Acr.UserDialogs;
using FastFoodApp.Modelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalHacerPedido : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;
        ToastConfigClass toastConfig = new ToastConfigClass();

        string totaldeproductos = "";
        public ModalHacerPedido()
        {
            InitializeComponent();
            LblTotal.Text = (App.TodalPrecioCarrito + App.envio).ToString();
            lblprecioenvio.Text = App.envio.ToString();
        }


        private async void BtnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea hacer su pedido?", "SI", "NO"))
            {
                int txtdinero = Convert.ToInt32(TxtDinero.Text);
                int lbltotal = Convert.ToInt32(LblTotal.Text);

                if (string.IsNullOrEmpty(TxtDinero.Text))
                {
                    toastConfig.MostrarNotificacion($"Por favor rellene todos los campos", ToastPosition.Top, 3, "#e63946");
                }
                else
                {
                    if (txtdinero < lbltotal)
                    {
                        LblCantidadmenor.IsVisible = true;
                    }
                    else
                    {
                        _ = EnviarPedido(Convert.ToInt32(TxtDinero.Text), Convert.ToInt32(lbldevuelta.Text), App.latitud, App.longitud, "PENDIENTE", App.idusuarios, App.NumeroOrdenGeneral);
                        SendNotification();
                        await PopupNavigation.PopAsync();
                        LblCantidadmenor.IsVisible = true;

                    }

                }
            }
            else
            {

            }
        }

        public async void SendNotification()
        {
            try
            {
                string token = "";
                string cliente = "";
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();

                var datos = await metodos.ObtenerTokens();

                foreach (var item in datos)
                {
                    token = item.token_firebase;
                    cliente = item.nombre; 

                    string serverKey = "AAAASjRa9I8:APA91bH_YuXdIGADCLI7GZ47-duGu11ZAiNyStVVGCnAAYxvMYIRaipGXVQZCUFn7wwo-yR-JIcLPmCu4un-OdGcpfYoFtNRlu77AfmrjVLRFfDjIWOJvJ3Xr1GRdbZchUvhO6zLM63Y";
                    string token_tecnico = token;
                    var result = "-1";
                    var webAddr = "https://fcm.googleapis.com/fcm/send";
                    string tittle, body;

                    tittle = $"Let's Eat Again - Alerta";
                    body = $"{cliente} ha realizado un pedido";


                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        string json = "{\"to\": \"" + token_tecnico + "\",\"notification\": {\"body\": \"" + body + "\",\"title\": \"" + tittle + "\"}}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                    // return result;
                }


            }
            catch (Exception ex)
            {
            }
        }

        public async Task InsertarIdPedido()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedido(App.idusuarios);
                SeleccionarNumeroDeOrdenGeneral();
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo realizar dicha acción, por favor verifique nuevamente.", ToastPosition.Top, 4, "#e63946");

            }
        }

        public async Task SeleccionarNumeroDeOrdenGeneral()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.SNumeroDeOrdenGeneral(App.idusuarios, "PRE-ORDEN");
                App.NumeroOrdenGeneral = datos[0].idpedidos_fast_food;
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");

            }
        }

        public async Task EnviarPedido(int concuantopagara, int devuelta, string latitud, string longitud, string estado_del_pedido, int idusuarios, int idpedidos_fast_food)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedido(concuantopagara, devuelta, latitud, longitud, estado_del_pedido, idusuarios, idpedidos_fast_food);

                if (datos.Respuesta == "OK")
                {
                    toastConfig.MostrarNotificacion($"Gracias por realizar su pedido", ToastPosition.Top, 3, "#4bbd62");
                    InsertarIdPedido();
                }
                else
                {
                    toastConfig.MostrarNotificacion($"No se pudo realizar su pedido, intente mas tarde", ToastPosition.Top, 3, "#e63946");

                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private void TxtDinero_TextChanged(object sender, TextChangedEventArgs e)
        {

            double.TryParse(TxtDinero.Text, out double dineroConvertido);

            if (!string.IsNullOrEmpty(dineroConvertido.ToString()))
            {
                int Dinerotxt = Convert.ToInt32(dineroConvertido);
                int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

                if (Dinerotxt < TotalTxt)
                {
                    lbldevuelta.Text = "0";
                }
                else
                {
                    if (string.IsNullOrEmpty(dineroConvertido.ToString()))
                    {
                        lbldevuelta.Text = "0";
                    }
                    else
                    {
                        int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                        int dinero = Convert.ToInt32(dineroConvertido);
                        int devuelta = dinero - total;
                        lbldevuelta.Text = devuelta.ToString();
                    }
                }

            }
        }
    }
}
