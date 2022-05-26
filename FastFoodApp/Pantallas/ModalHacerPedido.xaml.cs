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

        string totaldeproductos = "";
        public ModalHacerPedido()
        {
            InitializeComponent();
        }


        private async void BtnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea hacer su pedido?", "SI", "NO"))
            {

                if (string.IsNullOrEmpty(TxtDinero.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
                }

                EnviarPedido(App.nombre, App.correo, App.whatsapp, Convert.ToInt32(TxtDinero.Text), Convert.ToInt32(lbldevuelta.Text), App.direccion, App.TodosLosProductosDeLaOrden, App.latitud, App.longitud);
            }
            else
            {

            }

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
