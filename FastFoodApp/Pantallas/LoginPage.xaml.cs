using Acr.UserDialogs;
using FastFoodApp.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LlenarEmpresa();

        }

        public async void IniciarSesion()
        {
            BtnLogin.IsEnabled = false;

            using (UserDialogs.Instance.Loading("Cargando..."))
            {

                try
                {
                    if (string.IsNullOrEmpty(TxtEmail.Text) || string.IsNullOrEmpty(TxtPassword.Text))
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast("El usuario y la contraseña son obligatorios");
                        return;
                    }


                    FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                    var resultTecnico = await metodos.IniciarSesion(TxtEmail.Text, TxtPassword.Text);
                    if (resultTecnico.respuesta == "OK")
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast($"Bienvenido {resultTecnico.nombre}");
                        _ = this.Navigation.PopModalAsync();
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast($"Los datos no son correctos");
                    }
                }
                catch (Exception ex)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast($"No se pudo establecer la conexión, por favor verifique los datos nuevamente");
                }
                BtnLogin.IsEnabled = true;

            }
        }

        public async Task LlenarEmpresa()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerEmpresa();
                App.envio = datos[0].envio;
            }
            catch (Exception ex)
            {

            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            IniciarSesion();
            await Navigation.PushModalAsync(new PrincipalPage());
            App.nombre = "Marcos Bremont";
            App.rnc = "";
            App.direccion = "Calle Ramon Maria Piña, al lado de la mata de mango";
            App.telefono = "809-574-6213";
            App.whatsapp = "809-907-3244";
            App.correo = "MarcosBremont00@gmail.com";
            App.latitud = "19.121759";
            App.longitud = "-70.644988";
        }
    }
}