using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FastFoodApp.Entidad;
using FastFoodApp.Metodos;
using FastFoodApp.Modelo;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPageEmpresa : ContentPage
    {
        ModalCantidad modalCantidad = new ModalCantidad();
        ModalMenu modalMenu = new ModalMenu();
        ModalHacerPedido modalHacerPedido = new ModalHacerPedido();
        ToastConfigClass toastConfig = new ToastConfigClass();


        public PrincipalPageEmpresa()
        {
            InitializeComponent();
            btnMenuEmpresa.Source = "hamburgerSodaAmarillo.png";

            _ = LlenarMenu();

            gridPedidosEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPedidosEmpresa.IsVisible = true;
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;


                    btnPedidosEmpresa.Source = "TimeAmarillo";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";
                    _ = LlenarPedidos();
                }),
                NumberOfTapsRequired = 1
            });

            gridPedidosEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {

                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;


                    btnPedidosEmpresa.Source = "TimeAmarillo";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";
                    _ = LlenarPedidos();
                }),
                NumberOfTapsRequired = 1
            });

            gridInicioEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutPedidosEmpresa.IsVisible = false;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaAmarillo.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificacionesEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (App.empresa == "S")
                    {
                        StackLayoutPaginaPrincipal.IsVisible = false;
                        StackLayoutMiPerfil.IsVisible = false;
                        StackLayoutTuCarrito.IsVisible = false;
                        StackLayoutPedidosEmpresa.IsVisible = false;
                        StackLayoutNotificacionesEmpresa.IsVisible = true;
                    }
                    else
                    {
                        StackLayoutPaginaPrincipal.IsVisible = false;
                        StackLayoutMiPerfil.IsVisible = false;
                        StackLayoutTuCarrito.IsVisible = false;
                        StackLayoutPedidosEmpresa.IsVisible = false;
                        StackLayoutNotificacionesEmpresa.IsVisible = true;
                    }



                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellAmarillo";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificacionesEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = true;


                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellAmarillo";

                }),
                NumberOfTapsRequired = 1
            });

            gridMiPerfilEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userAmarillo";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    LlenarMiPerfil();

                }),
                NumberOfTapsRequired = 1
            });


            gridAnotarPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = true;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoAmarillo";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    _ = LlenarCarrito();

                }),
                NumberOfTapsRequired = 1
            });
        }

        private void LlenarMiPerfil()
        {
            TxtNombre.Text = App.nombre;
            TxtApellido.Text = App.apellido;
            TxtDireccion.Text = App.direccion;
            TxtTelefono.Text = App.telefono;
            TxtClave.Text = App.clave;
            TxtEmail.Text = App.correo;

        }

        public async Task LlenarMenu()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerMenu();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task LlenarCarrito()
        {
            try
            {
                lsv_Carrito.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_Carrito.ItemsSource = null;
                var datos = await metodos.ObtenerCarritoPorUsuario(App.NumeroOrdenGeneral, App.idusuarios, "PRE-ORDEN");
                if (datos.Count == 0)
                {
                    LblAunNoAgregasNada.IsVisible = true;
                }
                else
                {
                    LblAunNoAgregasNada.IsVisible = false;

                }
                lsv_Carrito.ItemsSource = datos;

                lsv_Carrito.IsVisible = true;
                App.TodalPrecioCarrito = datos.Sum(n => n.total_por_producto);

            }
            catch (Exception ex)
            {

            }
        }


        public async Task LlenarPedidos()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerCarritoPorUsuario(0, App.idusuarios, "PENDIENTE");
            }
            catch (Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerMenu();
            }
            catch (Exception ex)
            {

            }


        }

        [Obsolete]
        async void BtnAgregar_Clicked(System.Object sender, System.EventArgs e)
        {

            var b = (Button)sender;

            var ob = b.CommandParameter as EMenu;

            if (ob != null)

            {

                // retrieve the value from the ‘ob’ and continue your work.

            }
            App.Precio = ob.precio;
            App.producto = ob.nombre;
            App.idProducto = ob.idmenu_fast_food;
            modalCantidad = new ModalCantidad();
            modalCantidad.Disappearing += ModalCantidad_Disappearing;
            await PopupNavigation.PushAsync(modalCantidad);

        }

        private void ModalCantidad_Disappearing(object sender, EventArgs e)
        {
            App.producto = "";
        }

        void lsv_menu_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var product = (EMenu)e.SelectedItem;
            App.Precio = product.precio;
        }

        [Obsolete]
        private async void BtnHacerPedido_Clicked(object sender, EventArgs e)
        {
            modalHacerPedido = new ModalHacerPedido();
            modalHacerPedido.Disappearing += ModalHacerPedido_Disappearing;
            await PopupNavigation.PushAsync(modalHacerPedido);

        }

        private void ModalHacerPedido_Disappearing(object sender, EventArgs e)
        {

        }

        private async void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea cerrar sesión?", "SI", "NO"))
            {
                //DatosConfiguracion.GrabarDatosSesion("", "", "S");
                LoginPage loginPage = new LoginPage();
                await Navigation.PushModalAsync(loginPage);
                //DatosConfiguracion.EliminarDatosSesion();


            }
        }

        void btnEditarPerfil_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtEmail.IsEnabled = true;
            TxtDireccion.IsEnabled = true;
            TxtTelefono.IsEnabled = true;
            TxtClave.IsEnabled = true;
            btnGuardarCambios.IsVisible = true;
            btnEditarPerfil.IsVisible = false;

        }

        public async void ActualizarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string clave, int idusuarios)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ActualizarUsuario(nombre, apellido, direccion, telefono, email, clave, idusuarios);
            }
            catch (Exception ex)
            {

            }
        }

        void btnGuardarCambios_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtEmail.IsEnabled = false;
            TxtDireccion.IsEnabled = false;
            TxtTelefono.IsEnabled = false;
            btnEditarPerfil.IsVisible = true;
            TxtClave.IsEnabled = false;
            btnGuardarCambios.IsVisible = false;

            ActualizarUsuario(TxtNombre.Text, TxtApellido.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text, App.idusuarios);

        }

        void BtnAgregarAlMenu_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                AgregarProductoAlMenu(TxtNombreProducto.Text, Convert.ToInt32(TxtPrecio.Text), TxtDisponible.Text, "TxtDescripcion.Text", TxtDescripcion.Text);
                toastConfig.MostrarNotificacion($"Producto agregado al menú", ToastPosition.Top, 3, "#51C560");
                TxtNombreProducto.Text = "";
                TxtPrecio.Text = "";
                TxtDisponible.Text = "";
                TxtDescripcion.Text = "";
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"Ocurrio un error, intenta nuevamente o comunicate con el administrador", ToastPosition.Top, 3, "#51C560");

            }

        }



        private async void AgregarProductoAlMenu(string nombre, int precio, string disponibilidad, string foto, string descripcion)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarProductoAlMenu(nombre, precio, disponibilidad, foto, descripcion);
            }
            catch (Exception ex)
            {

            }
        }

        void BtnAgregarFoto_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void BtnVermenu_Clicked(System.Object sender, System.EventArgs e)
        {
            modalMenu = new ModalMenu();
            await PopupNavigation.PushAsync(modalMenu);
        }
    }
}