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
            _ = LlenarEmpresa();

        }

        public async void IniciarSesion()
        {

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
                    var result = await metodos.IniciarSesion(TxtEmail.Text, TxtPassword.Text);
                    if (result.respuesta == "OK")
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast($"Bienvenido {result.nombre}");
                        await Navigation.PushModalAsync(new PrincipalPage());
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert($"Los datos no son correctos");

                    }
                }
                catch (Exception ex)
                {

                    Acr.UserDialogs.UserDialogs.Instance.Toast($"No se pudo establecer la conexión, por favor verifique los datos nuevamente");
                }

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

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            IniciarSesion();

        }
    }
}