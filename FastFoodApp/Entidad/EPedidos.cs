using System;
using System.Collections.Generic;
using System.Text;

namespace FastFoodApp.Entidad
{
    public class EPedidos
    {
        public string nombre { get; set; }
        public int precio { get; set; }
        public string disponible { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        public string idpedidos_fast_food { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string cedula { get; set; }
        public int concuantopagara { get; set; }
        public int devuelta { get; set; }
        public string direccion { get; set; }
        public string producto { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string estado_del_pedido { get; set; }
    }
}
