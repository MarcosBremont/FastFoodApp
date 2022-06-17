using Acr.UserDialogs;
using FastFoodApp.Entidad;
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
    public partial class ModalNotificaciones : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;
        List<ENotificaciones> ListENotificaciones = new List<ENotificaciones>();
        ToastConfigClass toastConfig = new ToastConfigClass();

        string totaldeproductos = "";
        public ModalNotificaciones()
        {
            InitializeComponent();
            _ = LlenarNotificaciones();

        }

        public async Task LlenarNotificaciones()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerNotificaciones(0, 0.ToString());
                lsv_notificaciones.ItemsSource = datos;

            }
            catch (Exception ex)
            {

            }
        }

        async void lsv_notificaciones_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
            var result = await DisplayAlert("Aviso", "¿Desea ocultar esta notificación?", "SI", "NO");
            if (result)
            {
                var obj = (ENotificaciones)e.SelectedItem;
                var ide = obj.disponibilidad;

                if (ide == "S")
                {
                    var datos = await metodos.ActualizarNotificacion(obj.idnotificaciones_empresa, "N");
                    toastConfig.MostrarNotificacion($"Esta notificación ya no será visible para tus clientes", ToastPosition.Top, 3, "#51C560");
                }
                else
                {
                    var datos = await metodos.ActualizarNotificacion(obj.idnotificaciones_empresa, "S");
                    toastConfig.MostrarNotificacion($"Esta notificación ahora será visible para tus clientes", ToastPosition.Top, 3, "#51C560");

                }
            }
            _ = LlenarNotificaciones();


        }


        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }



    }
}
