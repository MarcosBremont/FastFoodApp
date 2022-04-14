using FastFoodApp.Pantallas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
