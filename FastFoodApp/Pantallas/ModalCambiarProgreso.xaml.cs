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

        string totaldeproductos = "";
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
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ActualizarProgresoPedido(idpedidos_fast_food, estado_del_pedido);
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnEnProceso_Clicked(object sender, EventArgs e)
        {
            ActualizarProgresoPedido(App.idpedidos_fast_food, "EN PROCESO");
        }

        private void BtnEnCamino_Clicked(object sender, EventArgs e)
        {
            ActualizarProgresoPedido(App.idpedidos_fast_food, "EN CAMINO");
        }

        private void BtnEntregada_Clicked(object sender, EventArgs e)
        {
            ActualizarProgresoPedido(App.idpedidos_fast_food, "ENTREGADA");
        }
    }
}
