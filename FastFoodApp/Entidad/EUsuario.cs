using System;
using System.Collections.Generic;
using System.Text;

namespace FastFoodApp.Entidad
{
    public class EUsuario
    {
        public int idusuarios { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string clave { get; set; }
        public string mac { get; set; }
        public string respuesta { get; set; }
        public string mensaje { get; set; }
        public string empresa { get; set; }
        public string foto { get; set; }
        public string result { get; set; }
        public bool encontrado { get; set; }
    }
}
