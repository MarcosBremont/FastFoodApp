using FastFoodApp.Pantallas;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp
{
    public partial class App : Application
    {
        public static int Precio { get; set; }
        public static int idpedidos_fast_food { get; set; }
        public static int TodalPrecioCarrito { get; set; }
        public static string TodalPrecioCarritoString { get; set; }
        public static int idusuarios { get; set; }
        public static int idProducto { get; set; }
        public static string url_foto_menu { get; set; }
        public static string url_foto_empresa { get; set; }
        public static int NumeroOrdenGeneral { get; set; }
        public static string nombre { get; set; }
        public static string empresa { get; set; }
        public static string descripcion { get; set; }
        public static string apellido { get; set; }
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
        public static int envio { get; set; }
        public static int devuelta { get; set; }
        public static string producto { get; set; }
        public static string DineroEnTxt { get; set; }
        public static string Token_Firebase { get; set; }
        public static StringBuilder TodosLosProductosDeLaOrden = new StringBuilder();

        public static List<StringBuilder> listOfStrings = new List<StringBuilder>();


        //Datos Usuario
        public static string Foto { get; set; }


        //Datos Pedido
        public static string longitudPedido { get; set; }
        public static string latitudPedido { get; set; }
        public static string ProgresoOrden { get; set; }
        public static DateTime FechaDesde { get; set; }
        public static DateTime FechaHasta { get; set; }
        public static string ProductoInhabilitado { get; set; }


        //DATOS EMPRESA
        public static int idempresa { get; set; }
        public static string NombreEmpresa { get; set; }
        public static string rncEmpresa { get; set; }
        public static string direccionEmpresa { get; set; }
        public static string telefonoEmpresa { get; set; }
        public static string whatsappEmpresa { get; set; }
        public static string correoEmpresa { get; set; }
        public static string latitudEmpresa { get; set; }
        public static string longitudeEmpresa { get; set; }
        public static string claveEmpresa { get; set; }
        public static string instagramEmpresa { get; set; }
        public static string encargado_Empresa { get; set; }
        public static string facebookEmpresa { get; set; }
        public static string logo_empresa { get; set; }



        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (!AppCenter.Configured)
                Push.PushNotificationReceived += Push_PushNotificationReceived;
            AppCenter.Start("ios={YOUR_IOS_APP_KEY};" +
                            "android={AAAA2wVoohs:APA91bFZbe-KlRTLdV-SMcLbx6OsMI1_EZQyzEZI9lEVy2jBvZDOOWbSMUpDQujxDgipDYCJM0BCp-M1_84BRghSllAV9lku9t_2IV3WKFCPA68yTOgRASKEi1NC5bF9sPSMAbj194S4};",
                            typeof(Push));
        }

        private void Push_PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            // Add the notification message and title to the message
            var summary = $"Push notification received:" +
                                $"\n\tNotification title: {e.Title}" +
                                $"\n\tMessage: {e.Message}";

            // If there is custom data associated with the notification,
            // print the entries
            if (e.CustomData != null)
            {
                summary += "\n\tCustom data:\n";
                foreach (var key in e.CustomData.Keys)
                {
                    summary += $"\t\t{key} : {e.CustomData[key]}\n";
                }
            }

            // Send the notification summary to debug output
            System.Diagnostics.Debug.WriteLine(summary);

        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
