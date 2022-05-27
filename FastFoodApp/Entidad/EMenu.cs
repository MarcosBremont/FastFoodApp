using System;
using System.Collections.Generic;
using System.Text;

namespace FastFoodApp.Entidad
{
    public class EMenu
    {
        public int idmenu_fast_food { get; set; }
        public string nombre { get; set; }
        public int precio { get; set; }
        public int total_por_producto { get; set; }
        public string disponible { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
    }
}
