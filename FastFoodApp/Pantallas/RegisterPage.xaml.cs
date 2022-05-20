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
    public partial class RegisterPage : ContentPage
    {
        string latitud = "";
        string longitud = "";
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


        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TxtNombre.Text) || string.IsNullOrEmpty(TxtApellido.Text) || string.IsNullOrEmpty(TxtDireccion.Text) || string.IsNullOrEmpty(TxtTelefono.Text) || string.IsNullOrEmpty(TxtEmail.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
            }
            Localizar();

            RegistrarUsuario(TxtNombre.Text, TxtApellido.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, latitud, longitud, TxtPassword.Text);

        }

        private async void RegistrarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string latitud, string longitud, string clave)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.RegistrarUsuario(nombre, apellido, direccion, telefono, email, latitud, longitud, clave);
            }
            catch (Exception ex)
            {

            }
        }
    }
}