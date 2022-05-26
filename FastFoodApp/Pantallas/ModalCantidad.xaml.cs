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


                    if (string.IsNullOrEmpty(cantidad.ToString()))
                    {
                        LblTotal.Text = App.Precio.ToString();
                    }
                    else
                    {

                        int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                    }
                }

            }

        }




        async void BtnagregarCantidad_Clicked(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCantidad.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene el campo de cantidad");
            }

            if (await DisplayAlert("Información", "¿Desea agregar a su carrito?", "SI", "NO"))
            {
                AgregarPedidoTemporal();
                //App.TodosLosProductosDeLaOrden = App.TodosLosProductosDeLaOrden.Append(TxtCantidad.Text + App.producto + "-");

                Acr.UserDialogs.UserDialogs.Instance.Toast(App.producto + " Agregada a tu orden.");
                await PopupNavigation.PopAsync();
            }
        }

        public async Task AgregarPedidoTemporal()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedidoTemporal(App.idProducto, App.idusuarios, Convert.ToInt32(TxtCantidad.Text));
            }
            catch (Exception ex)
            {

            }
        }

        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
            TxtCantidad.Text = "";
        }



    }
}
