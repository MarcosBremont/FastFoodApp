using Acr.UserDialogs;
using FastFoodApp.Entidad;
using FastFoodApp.Modelo;
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
        ToastConfigClass toastConfig = new ToastConfigClass();
        public LoginPage()
        {
            InitializeComponent();
            _ = LlenarEmpresa();

            LayoutRegistrate.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushModalAsync(new RegisterPage());

                }),
                NumberOfTapsRequired = 1
            });

            LayoutOlvideMiContraseña.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushModalAsync(new OlvideMiContrasena());

                }),
                NumberOfTapsRequired = 1
            });


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
                        App.empresa = result.empresa;
                        InsertarIdPedido();
                        SeleccionarNumeroDeOrdenGeneral();
                        LlenarEmpresa();

                        toastConfig.MostrarNotificacion($"Bienvenido {result.nombre}", ToastPosition.Top, 3, "#51C560");
                        if (result.empresa == "S")
                        {
                            await Navigation.PushModalAsync(new PrincipalPageEmpresa());
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new PrincipalPage());
                        }
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
                App.NombreEmpresa = datos[0].nombre;
                App.direccionEmpresa = datos[0].direccion;
                App.rncEmpresa = datos[0].rnc;
                App.telefonoEmpresa = datos[0].telefono;
                App.whatsappEmpresa = datos[0].whatsapp;
                App.correoEmpresa = datos[0].correo;
                App.latitudEmpresa = datos[0].latitud;
                App.longitudeEmpresa = datos[0].longitud;
                App.claveEmpresa = datos[0].clave;
                App.instagramEmpresa = datos[0].instagram;
                App.facebookEmpresa = datos[0].facebook;
                App.encargado_empresa = datos[0].encargado_empresa;
                App.encargado_empresa = datos[0].encargado_empresa;
                App.logo_empresa = datos[0].logo_empresa;
                App.idempresa = datos[0].idempresa;
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SeleccionarNumeroDeOrdenGeneral()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.SNumeroDeOrdenGeneral(App.idusuarios, "PRE-ORDEN");
                App.NumeroOrdenGeneral = datos[0].idpedidos_fast_food;
            }
            catch (Exception ex)
            {

            }
        }

        public async Task InsertarIdPedido()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedido(App.idusuarios);
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        private void eyebuttonnegro_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = false;
            eyebuttonazul.IsVisible = true;
            TxtPassword.IsPassword = false;
        }

        private void eyebuttonazul_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = true;
            eyebuttonazul.IsVisible = false;
            TxtPassword.IsPassword = true;
        }
    }
}