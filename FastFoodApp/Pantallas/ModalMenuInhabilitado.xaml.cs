using FastFoodApp.Entidad;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalMenuInhabilitado : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;
        List<EMenu> ListMenu = new List<EMenu>();

        public ModalMenuInhabilitado()
        {
            InitializeComponent();
            _ = LlenarMenu();

        }

        public async Task LlenarMenu()
        {
            try
            {
                lsv_menu.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_menu.ItemsSource = null;
                var datos = await metodos.ObtenerMenu("N");
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;
                ListMenu = datos;

            }
            catch (Exception ex)
            {

            }
        }


        void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var ProductoMenu = ListMenu.Where(c => c.nombre.Contains(ProductosSearchBar.Text.ToUpper()));
            lsv_menu.ItemsSource = ProductoMenu;
        }


        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }



        async void lsv_menu_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea mostrar este producto?", "SI", "NO"))
            {
                try
                {
                    var IDMenu = (EMenu)e.SelectedItem;
                    FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                    var datos = await metodos.ActualizarMenu(IDMenu.idmenu_fast_food, "S");
                    LlenarMenu();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

    }
}
