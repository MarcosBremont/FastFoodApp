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
            LlenarMenu();



            gridPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = true;
                    btnPedidos.Source = "buy1";
                    btnMenu.Source = "hamburger3";

                }),
                NumberOfTapsRequired = 1
            });

            gridInicio.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutPedidos.IsVisible = false;
                    btnPedidos.Source = "buy2";
                    btnMenu.Source = "hamburger1";

                }),
                NumberOfTapsRequired = 1
            });

            gridMiPerfil.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    btnPedidos.Source = "user1";
                    btnMenu.Source = "hamburger1";
                    btnPedidos.Source = "buy1";

                }),
                NumberOfTapsRequired = 1
            });
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
    }
}