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
                        toastConfig.MostrarNotificacion($"El usuario y la contraseña son obligatorios.", ToastPosition.Top, 3, "#e63946");
                        return;
                    }


                    FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                    var result = await metodos.IniciarSesion(TxtEmail.Text, TxtPassword.Text);
                    if (result.respuesta == "OK")
                    {
                        App.empresa = result.empresa;
                        _ = InsertarIdPedido();
                        _ = SeleccionarNumeroDeOrdenGeneral();
                        _ = LlenarEmpresa();

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
                        toastConfig.MostrarNotificacion($"Los datos no son correctos, por favor verifique nuevamente.", ToastPosition.Top, 4, "#e63946");
                    }
                }
                catch (Exception ex)
                {
                    toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor verifique los datos nuevamente.", ToastPosition.Top, 4, "#e63946");
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
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");
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
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");

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
                toastConfig.MostrarNotificacion($"No se pudo realizar dicha acción, por favor verifique nuevamente.", ToastPosition.Top, 4, "#e63946");

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