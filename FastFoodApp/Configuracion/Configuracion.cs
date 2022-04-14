using System;
using System.Collections.Generic;
using System.Text;

namespace FastFoodApp.Configuracion
{
    public class Configuracion
    {
        //Conexion de la Api
        public static string Server = "http://apibremont.tecnolora.com/";//Conexion Api Real
        private static string ApiName = "Api/";

        public static string ServerApi { get { return Server + ApiName; } }

        public Configuracion()
        {
        }

    }
}
