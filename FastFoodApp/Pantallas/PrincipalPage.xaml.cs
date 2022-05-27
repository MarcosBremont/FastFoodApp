﻿using System;
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
            _ = LlenarMenu();

            gridPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(()=>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;


                    btnPedidos.Source = "TimeAmarillo";
                    btnMenu.Source = "HamburgerSodaWhite";
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
                    btnMenu.Source = "HamburgerSodaAmarillo";
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
                    btnMenu.Source = "HamburgerSodaWhite";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellAmarillo";


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
                    btnMenu.Source = "HamburgerSodaWhite";
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
                    btnMenu.Source = "HamburgerSodaWhite";
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
    }
}