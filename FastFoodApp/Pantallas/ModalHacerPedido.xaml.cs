using Acr.UserDialogs;
using FastFoodApp.Modelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
                if (txtdinero > lbltotal)
                {
                    toastConfig.MostrarNotificacion($"La cantidad introducida es menor al precio total", ToastPosition.Top, 3, "#e63946");
                }

                if (string.IsNullOrEmpty(TxtDinero.Text))
                {
                    toastConfig.MostrarNotificacion($"Por favor rellene todos los campos", ToastPosition.Top, 3, "#e63946");
                }

                _ = EnviarPedido(Convert.ToInt32(TxtDinero.Text), Convert.ToInt32(lbldevuelta.Text), App.latitud, App.longitud, "PENDIENTE", App.idusuarios, App.NumeroOrdenGeneral);
            }
            else
            {

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
