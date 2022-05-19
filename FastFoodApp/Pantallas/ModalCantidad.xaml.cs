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
    public partial class ModalCantidad : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        string totaldeproductos = "";
        public ModalCantidad()
        {
            InitializeComponent();
            LblTotal.Text = "RD$ " + (App.Precio + App.envio).ToString();
            lblprecioenvio.Text = "RD$" + App.envio.ToString() + " Pesos";
            lblprecioenvio.Text.Trim();
            TxtCantidad.Text = "1";
        }

        public async Task EnviarPedido(string usuario, string email, string telefono, int concuantopagara, int devuelta, string direccion, StringBuilder producto, string latitud, string longitud)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedido(usuario, email, telefono, concuantopagara, devuelta, direccion, producto, latitud, longitud);
            }
            catch (Exception ex)
            {

            }
        }

        async void BtnagregarCantidad_Clicked(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCantidad.Text) || string.IsNullOrEmpty(TxtDinero.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
            }
            //App.listOfStrings.Add(App.producto);
            App.TodosLosProductosDeLaOrden = App.TodosLosProductosDeLaOrden.Append(App.producto + "-");
                //App.listOfStrings[0];
            Acr.UserDialogs.UserDialogs.Instance.Toast(App.producto + "Agregada a tu orden.");
        }

        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
            TxtCantidad.Text = "";
            TxtDinero.Text = "";
        }

        void TxtCantidad_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

            double.TryParse(TxtCantidad.Text, out double cantidad);

            if (!string.IsNullOrEmpty(cantidad.ToString()))
            {
                if (cantidad == 0)
                {
                    LblTotal.Text = "0";
                }
                else
                {

                    int totalcantidad = Convert.ToInt32(cantidad);
                    LblTotal.Text = (App.Precio * totalcantidad + App.envio).ToString();

                    if (!string.IsNullOrEmpty(TxtDinero.Text))
                    {
                        int Dinerotxt = Convert.ToInt32(TxtDinero.Text);

                        int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));



                        if (string.IsNullOrEmpty(cantidad.ToString()))
                        {
                            LblTotal.Text = App.Precio.ToString();
                            lbldevuelta.Text = "0";
                        }
                        else
                        {

                            int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                            int dinero = Convert.ToInt32(TxtDinero.Text);
                            int devuelta = dinero - total;
                            if (cantidad != 0)
                            {
                                lbldevuelta.Text = devuelta.ToString();
                            }
                            else
                            {
                                lbldevuelta.Text = "0";
                            }
                        }
                        if (Dinerotxt < TotalTxt)
                        {
                            TxtDinero_TextChanged(sender, e);

                        }
                    }

                }

            }
            else
            {
                LblTotal.Text = "0";
            }



        }

        private void TxtDinero_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtDinero.Text = App.DineroEnTxt;
            double.TryParse(TxtDinero.Text, out double dineroConvertido);

            if (!string.IsNullOrEmpty(dineroConvertido.ToString()))
            {
                int Dinerotxt = Convert.ToInt32(dineroConvertido);
                int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

                if (Dinerotxt < TotalTxt)
                {
                    //int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                    //int dinero = Convert.ToInt32(dineroConvertido);
                    //int devuelta = dinero - total;
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
            App.DineroEnTxt = TxtDinero.Text;
        }

        private async void BtnHacerPedido_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea hacer su pedido?", "SI", "NO"))
            {
                if (string.IsNullOrEmpty(TxtCantidad.Text) || string.IsNullOrEmpty(TxtDinero.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
                }
                EnviarPedido(App.nombre, App.correo, App.whatsapp, Convert.ToInt32(TxtDinero.Text), Convert.ToInt32(lbldevuelta.Text), App.direccion, App.TodosLosProductosDeLaOrden, App.latitud, App.longitud);
            }
            else
            {

            }
          
        }
    }
}
