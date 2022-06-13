using FastFoodApp.Configuracion;
using FastFoodApp.Entidad;
using FastFoodApp.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodApp.Metodos
{
    public class Metodos
    {
        Herramientas herramientas = new Herramientas();

        public Metodos()
        {
            // constructor
        }

        public async Task<List<EMenu>> ObtenerMenu()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerMenu");
            var lista_menu = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EMenu>>(result);

            return lista_menu;
        } // Fin del método ObtenerMenu


        public async Task<List<EEmpresa>> ObtenerEmpresa()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerEmpresa");
            var lista_empresa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EEmpresa>>(result);

            return lista_empresa;
        } // Fin del método ObtenerEmpresa



        public async Task<Result> AgregarPedido(int concuantopagara, int devuelta, string latitud, string longitud, string estado_del_pedido, int idusuarios, int idpedidos_fast_food)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ActualizarPedido/{concuantopagara}/{devuelta}/{latitud}/{longitud}/{estado_del_pedido}/{idusuarios}/{idpedidos_fast_food}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<EUsuario> IniciarSesion(string email, string password)
        {

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/IniciarSesion/{email}/{password}");
            var UsuarioResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EUsuario>(result);

            if (UsuarioResult.respuesta == "OK")
            {
                App.idusuarios = UsuarioResult.idusuarios;
                App.nombre = UsuarioResult.nombre;
                App.apellido = UsuarioResult.apellido;
                App.direccion = UsuarioResult.direccion;
                App.telefono = UsuarioResult.telefono;
                App.correo = UsuarioResult.correo;
                App.latitud = UsuarioResult.latitud;
                App.longitud = UsuarioResult.longitud;
                App.clave = UsuarioResult.clave;
                App.empresa = UsuarioResult.empresa;
            }

            return UsuarioResult;
        }

        public async Task<List<EPedidos>> ObtenerPedidos(string estado_del_pedido, int idusuarios)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerPedidos/{estado_del_pedido.ToUpper()}/{idusuarios}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<Result> RegistrarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string latitud, string longitud, string clave)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/RegistratUsuario/{nombre.ToUpper()}/{apellido.ToUpper()}/{direccion.ToUpper()}/{telefono}/{email.ToUpper()}/{direccion.ToUpper()}/{latitud}/{latitud}/{longitud}/{clave.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<List<EPedidos>> ObtenerCarritoPorUsuario(int idpedidos, int idusuario, string estado)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerCarritoPorUsuario/{idpedidos}/{idusuario}/{estado}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<List<EPedidos>> AgregarPedido(int idusuarios)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedido/{idusuarios}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<Result> ActualizarPedido(int concuantopagara, int devuelta, int latitud, string longitud, string estado_del_pedido, int idusuarios, int idpedidos_fast_food)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/RegistratUsuario/{concuantopagara}/{devuelta}/{longitud}/{estado_del_pedido}/{idusuarios}/{idpedidos_fast_food}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<Result> AgregarPedidoPorID(int idusuarios)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedido/{idusuarios}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa

        public async Task<Result> AgregarPedidoTemporal(int idmenu_fast_food, int idusuarios, int cantidad, string descripcion)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedidoTemporal/{idmenu_fast_food}/{idusuarios}/{cantidad}/{descripcion}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa

        public async Task<List<EPedidos>> SNumeroDeOrdenGeneral(int idusuario, string estado)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/SNumeroDeOrdenGeneral/{idusuario}/{estado}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<Result> ActualizarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string clave, int idusuarios)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ActualizarUsuario/{nombre}/{apellido}/{direccion}/{telefono}/{email}/{clave}/{idusuarios}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa

        public async Task<Result> AgregarProductoAlMenu(string nombre, int precio, string disponibilidad, string foto, string descripcion)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarProductoAlMenu/{nombre}/{precio}/{disponibilidad}/{foto}/{descripcion}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método AgregarProductoAlMenu


    }
}
