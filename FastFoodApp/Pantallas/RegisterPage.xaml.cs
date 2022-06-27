using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FastFoodApp.Modelo;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        string latitud = "";
        string longitud = "";
        ToastConfigClass toastConfig = new ToastConfigClass();

        public RegisterPage()
        {
            InitializeComponent();
            LayoutIniciarSesion.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PopModalAsync();

                }),
                NumberOfTapsRequired = 1
            });

        }

        private async void Localizar()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                });
                latitud = location.Latitude.ToString();
                longitud = location.Longitude.ToString();

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("No es posible obtener la localización en este momento.");
            }

        }


        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TxtNombre.Text) || string.IsNullOrEmpty(TxtApellido.Text) || string.IsNullOrEmpty(TxtDireccion.Text) || string.IsNullOrEmpty(TxtTelefono.Text) || string.IsNullOrEmpty(TxtEmail.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
            }
            Localizar();

            RegistrarUsuario(TxtNombre.Text, TxtApellido.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, latitud, longitud, TxtPassword.Text);

        }

        private async void RegistrarUsuario(string nombre, string apellido, string direccion, string telefono, string correo, string latitud, string longitud, string clave)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.RegistrarUsuario(nombre, apellido, direccion, telefono, correo, latitud, longitud,clave);
                if (datos.Respuesta == "OK")
                {
                    toastConfig.MostrarNotificacion($"Registro completado exitosamente", ToastPosition.Top, 3, "#4bbd62");
                }
                else
                {
                    toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void BtnVolver_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();

        }
    }
}