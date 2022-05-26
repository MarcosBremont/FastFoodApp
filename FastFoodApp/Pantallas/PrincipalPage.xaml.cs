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

        public PrincipalPage()
        {
            InitializeComponent();
            _ = LlenarMenu();
            LlenarMiPerfil();
            _ = LlenarPedidos();
            _ = LlenarCarrito();


            gridPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(()=>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;

                    btnPedidos.Source = "pedidosamarillo";
                    btnMenu.Source = "hamburger3";
                    btnMiPerfil.Source = "user2";
                    btnImgCarrito.Source = "buy2";

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

                    StackLayoutPedidos.IsVisible = false;
                    btnPedidos.Source = "pedidosazul";
                    btnMenu.Source = "hamburger1";
                    btnMiPerfil.Source = "user2";
                    btnImgCarrito.Source = "buy2";

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

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidos.Source = "pedidosazul";
                    btnMenu.Source = "hamburger3";
                    btnMiPerfil.Source = "user1";
                    btnImgCarrito.Source = "buy2";

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
                    btnPedidos.Source = "pedidosazul";
                    btnMenu.Source = "hamburger3";
                    btnMiPerfil.Source = "user2";
                    btnImgCarrito.Source = "buy1";
                }),
                NumberOfTapsRequired = 1
            });
        }

        private void LlenarMiPerfil()
        {
            LblNombre.Text = App.nombre;
            LblApellido.Text = App.apellido;
            lblDireccion.Text = App.direccion;
            LblTelefono.Text = App.telefono;
            LblEmail.Text = App.correo;

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

        public async Task LlenarCarrito()
        {
            try
            {
                lsv_Carrito.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_Carrito.ItemsSource = null;
                var datos = await metodos.ObtenerCarritoPorUsuario(App.NumeroOrdenGeneral,App.idusuarios, "PRE-ORDEN");
                lsv_Carrito.ItemsSource = datos;
                lsv_Carrito.IsVisible = true;
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
                var datos = await metodos.ObtenerCarritoPorUsuario(App.NumeroOrdenGeneral, App.idusuarios, "PENDIENTE");
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
            App.idProducto = ob.
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


        private void TxtDinero_TextChanged(object sender, TextChangedEventArgs e)
        {


            double.TryParse(TxtDinero.Text, out double dineroConvertido);

            if (!string.IsNullOrEmpty(dineroConvertido.ToString()))
            {
                int Dinerotxt = Convert.ToInt32(dineroConvertido);
                int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

                if (Dinerotxt < TotalTxt)
                {
                    lbldevuelta.Text = "0";
                }
                else
                {
                    if (string.IsNullOrEmpty(dineroConvertido.ToString()))
                    {
                        lbldevuelta.Text = "0";
                    }
                    else
                    {
                        int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                        int dinero = Convert.ToInt32(dineroConvertido);
                        int devuelta = dinero - total;
                        lbldevuelta.Text = devuelta.ToString();
                    }
                }

            }
        }

        public async Task EnviarPedido(string usuario, string email, string telefono, int concuantopagara, int devuelta, string direccion, StringBuilder producto, string latitud, string longitud)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarPedido(usuario, email, telefono, concuantopagara, devuelta, direccion, producto, latitud, longitud);
            }
            catch (Exception ex)
            {

            }
        }
        private async void BtnHacerPedido_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea hacer su pedido?", "SI", "NO"))
            {

                if (string.IsNullOrEmpty(TxtDinero.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
                }

                EnviarPedido(App.nombre, App.correo, App.whatsapp, Convert.ToInt32(TxtDinero.Text), Convert.ToInt32(lbldevuelta.Text), App.direccion, App.TodosLosProductosDeLaOrden, App.latitud, App.longitud);
            }
            else
            {

            }

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
    }
}