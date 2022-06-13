using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalMenu : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        public ModalMenu()
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
                var datos = await metodos.ObtenerMenu();
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;


            }
            catch (Exception ex)
            {

            }
        }


        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        void lsv_menu_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

        }


    }
}
