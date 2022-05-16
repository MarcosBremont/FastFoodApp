using FastFoodApp.Entidad;
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
        public LoginPage()
        {
            InitializeComponent();
            LlenarEmpresa();

        }

        public async Task LlenarEmpresa()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerEmpresa();
                App.envio = datos[0].envio;
            }
            catch (Exception ex)
            {

            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PrincipalPage());
        }
    }
}