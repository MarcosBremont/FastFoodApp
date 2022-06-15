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
    public partial class ModalCambiarProgreso : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;
        ToastConfigClass toastConfig = new ToastConfigClass();

        string btnname = "";
        public ModalCambiarProgreso()
        {
            InitializeComponent();
        }
        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        public async void ActualizarProgresoPedido(int idpedidos_fast_food, string estado_del_pedido)
        {
            try
            {
                if (await DisplayAlert($"Información", $"¿Deseas cambiar el pedido a {btnname}?", "SI", "NO"))
                {
                    FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                    var datos = await metodos.ActualizarProgresoPedido(idpedidos_fast_food, estado_del_pedido);
                    if (datos.Respuesta == "OK")
                    {
                        toastConfig.MostrarNotificacion($"Se ha actualizado el progreso", ToastPosition.Top, 3, "#51C560");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnEnProceso_Clicked(object sender, EventArgs e)
        {
            btnname = BtnEnProceso.Text;
            ActualizarProgresoPedido(App.idpedidos_fast_food, "EN PROCESO");
        }

        private void BtnEnCamino_Clicked(object sender, EventArgs e)
        {
            btnname = BtnEnCamino.Text;
            ActualizarProgresoPedido(App.idpedidos_fast_food, "EN CAMINO");

        }

        private void BtnEntregada_Clicked(object sender, EventArgs e)
        {
            btnname = BtnEntregada.Text;
            ActualizarProgresoPedido(App.idpedidos_fast_food, "ENTREGADA");

        }
    }
}
