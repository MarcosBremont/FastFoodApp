using FastFoodApp.Pantallas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp
{
    public partial class App : Application
    {
        public static int Precio { get; set; }
        public static string nombre { get; set; }
        public static string rnc { get; set; }
        public static string direccion { get; set; }
        public static string telefono { get; set; }
        public static string whatsapp { get; set; }
        public static string correo { get; set; }
        public static string latitud { get; set; }
        public static string longitud { get; set; }
        public static string clave { get; set; }
        public static string instagram { get; set; }
        public static string facebook { get; set; }
        public static string encargado_empresa { get; set; }
        public static string envio { get; set; }

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
