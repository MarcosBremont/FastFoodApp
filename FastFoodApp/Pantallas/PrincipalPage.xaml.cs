using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastFoodApp.Metodos;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPage : ContentPage
    {

        public PrincipalPage()
        {
            InitializeComponent();
            LlenarPagosRealizados();
        }

        public async Task LlenarPagosRealizados()
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

    }
}