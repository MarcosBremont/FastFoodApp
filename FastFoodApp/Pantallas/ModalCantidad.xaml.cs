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
    public partial class ModalCantidad : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        string totaldeproductos = "";
        ToastConfigClass toastConfig = new ToastConfigClass();

        public ModalCantidad()
        {
            InitializeComponent();
            LblTotal.Text = "RD$ " + App.Precio.ToString();
            //lblprecioenvio.Text = "RD$" + App.envio.ToString() + " Pesos";
            //lblprecioenvio.Text.Trim();
            TxtCantidad.Text = "1";

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

                await PopupNavigation.PopAsync();
                toastConfig.MostrarNotificacion($"Producto agregado a tu orden.", ToastPosition.Top, 3, "#4bbd62");

            }
        }
    
        public async Task AgregarPedidoTemporal()
        {
            try
            {
                string descripcion = TxtDescript.Text;
                if (string.IsNullOrEmpty(descripcion))
                {
                    descripcion = "Nada";
                }
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedidoTemporal(App.idProducto, App.idusuarios, Convert.ToInt32(TxtCantidad.Text), descripcion.ToUpper());

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
