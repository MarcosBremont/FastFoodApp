using Acr.UserDialogs;
using FastFoodApp.Metodos;
using FastFoodApp.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OlvideMiContrasena : ContentPage
    {
        ToastConfigClass toastConfig = new ToastConfigClass();

        public OlvideMiContrasena()
        {
            InitializeComponent();
        }

        private async void BtnEnviarContrasena_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Cargando..."))
            {

                try
                {


                    if (string.IsNullOrEmpty(TxtEmail.Text))
                    {
                        toastConfig.MostrarNotificacion("Recuerde rellenar todos los campos");
                        return;
                    }

                    FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                    var apiResult = await metodos.ResetearClave(new Entidad.EUsuario() { correo = TxtEmail.Text });

                    if (apiResult.Respuesta == "OK")
                    {
                        toastConfig.MostrarNotificacion($"La contraseña aleatoria ha sido enviada, por favor verifica tu Email", ToastPosition.Top, 3, "#51C560");
                        TxtEmail.Text = "";
                    }
                    else
                    {
                        toastConfig.MostrarNotificacion(apiResult.Mensaje, ToastPosition.Top, 3, "#51C560");
                    }



                }
                catch (Exception ex)
                {
                    toastConfig.MostrarNotificacion("No se pudo establecer la conexión, por favor verifique los datos nuevamente.", ToastPosition.Top, 3, "#FC412F");
                }
            }
        }

        private async void BtnVolverAtras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

        }
    }
}