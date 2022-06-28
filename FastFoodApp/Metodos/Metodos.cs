using FastFoodApp.Configuracion;
using FastFoodApp.Entidad;
using FastFoodApp.Modelo;
using Newtonsoft.Json;
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

        public async Task<List<EMenu>> ObtenerMenu(string disponible)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerMenu/{disponible}");
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


        public async Task<EUsuario> IniciarSesion(string email, string password, string Token_Firebase)
        {

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/IniciarSesion/{email}/{password}/{App.Token_Firebase}");
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
                App.Foto = UsuarioResult.foto;
            }

            return UsuarioResult;
        }

        public async Task<List<EPedidos>> ObtenerPedidos(string estado_del_pedido, int idusuarios, DateTime fechadesde, DateTime fechahasta )
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerPedidos/{estado_del_pedido.ToUpper()}/{idusuarios}/{fechadesde.ToString("yyyy-MM-dd 00:00:00")}/{fechahasta.ToString("yyyy-MM-dd 23:59:59")}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<Result> RegistrarUsuario(string nombre, string apellido, string direccion, string telefono, string correo, string latitud, string longitud, string clave)
        {
            if (string.IsNullOrEmpty(latitud))
            {
                latitud = "0";
            }
            if (string.IsNullOrEmpty(longitud))
            {
                longitud = "0";
            }

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/RegistrarUsuario/{nombre.ToUpper()}/{apellido.ToUpper()}/{direccion.ToUpper()}/{telefono}/{correo.ToUpper()}/{latitud}/{longitud}/{clave.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<List<EPedidos>> ObtenerCarritoPorUsuario(int idpedidos, int idusuario, string estado)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerCarritoPorUsuario/{idpedidos}/{idusuario}/{estado}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<EPedidos>AgregarPedido(int idusuario)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedido/{idusuario}");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<EPedidos>(result);

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

        public async Task<Result> ResetearClave(Entidad.EUsuario eUsuario)
        {
            var body_data = JsonConvert.SerializeObject(eUsuario);

            var result = await herramientas.EjecutarMetodoPost($"FastFood/ResetearClave", body_data);
            var apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return apiResult;
        }

        public async Task<Result> EnviarNotificacon(string mensaje, string disponibilidad)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/EnviarNotificacion/{mensaje}/{disponibilidad}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método AgregarProductoAlMenu

        public async Task<List<ENotificaciones>> ObtenerNotificaciones(int idnotificaciones_empresa, string disponibilidad)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/SNotificacionesFFA/{idnotificaciones_empresa}/{disponibilidad}");
            var lista_notificaciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ENotificaciones>>(result);

            return lista_notificaciones;
        } // Fin del método ObtenerMenu

        public async Task<Result> ActualizarNotificacion(int idnotificaciones_empresa, string disponibilidad)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ActualizarNotificacion/{idnotificaciones_empresa}/{disponibilidad}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método AgregarProductoAlMenu

        public async Task<Result> ActualizarProgresoPedido(int idpedidos_fast_food, string estado_del_pedido)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/UProgresoPedido/{idpedidos_fast_food}/{estado_del_pedido}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método AgregarProductoAlMenu

        public async Task<Result> ActualizarEmpresa(string nombreEmpresa, string DireccionEmpresa, string TelefonoEmpresa, string WhatsappEmpresa, string CorreoEmpresa, string PrecioEnvio, string ClaveEMpresa, int idempresa)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ActualizarEmpresa/{nombreEmpresa}/{DireccionEmpresa}/{TelefonoEmpresa}/{WhatsappEmpresa}/{CorreoEmpresa}/{PrecioEnvio}/{ClaveEMpresa}/{idempresa}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<Result> ActualizarMenu(int idmenu_fast_food, string disponible)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/UMenu/{idmenu_fast_food}/{disponible}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa

        public async Task<List<EUsuario>> ObtenerTokens()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/STokensUsuarios");
            var lista_tokens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EUsuario>>(result);

            return lista_tokens;
        } // Fin del método ObtenerMenu

    }
}
