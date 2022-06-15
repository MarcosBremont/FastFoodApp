using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastFoodApp.Entidad;
using FastFoodApp.Metodos;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPage : ContentPage
    {
        ModalCantidad modalCantidad = new ModalCantidad();
        ModalHacerPedido modalHacerPedido = new ModalHacerPedido();

        public PrincipalPage()
        {
            InitializeComponent();
            btnMenu.Source = "hamburgerSodaAmarillo.png";

            _ = LlenarMenu();

            gridPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {

                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;

                    btnPedidos.Source = "TimeAmarillo";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";
                    _ = LlenarPedidos();
                }),
                NumberOfTapsRequired = 1
            });


            gridInicio.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;


                    StackLayoutPedidos.IsVisible = false;
                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaAmarillo.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificaciones.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = true;

                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellAmarillo";
                    _ = LlenarNotificaciones();

                }),
                NumberOfTapsRequired = 1
            });

            gridMiPerfil.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userAmarillo";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";

                    LlenarMiPerfil();

                }),
                NumberOfTapsRequired = 1
            });


            gridMiCarrito.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = true;
                    StackLayoutNotificaciones.IsVisible = false;

                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoAmarillo";
                    btnNotitifaciones.Source = "bellWhite";

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
                lsv_menu.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_menu.ItemsSource = null;
                var datos = await metodos.ObtenerMenu();
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;


            }
            catch (Exception ex)
            {

            }
        }

        public async Task LlenarNotificaciones()
        {
            try
            {
                lsv_notificaciones.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_notificaciones.ItemsSource = null;
                var datos = await metodos.ObtenerNotificaciones(0,"S");
                lsv_notificaciones.ItemsSource = datos;
                lsv_notificaciones.IsVisible = true;


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
                lsv_pedidos.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_pedidos.ItemsSource = null;
                var datos = await metodos.ObtenerCarritoPorUsuario(0, App.idusuarios, "PENDIENTE");
                lsv_pedidos.ItemsSource = datos;
                lsv_pedidos.IsVisible = true;
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
                lsv_menu.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_menu.ItemsSource = null;
                var datos = await metodos.ObtenerMenu();
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;

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
    }
}