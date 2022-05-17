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



        public async Task<Result> AgregarPedido(string usuario, string email, string telefono, int concuantopagara, int devuelta, string direccion, string producto, string latitud, string longitud)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedido/{usuario.ToUpper()}/{email.ToUpper()}/{telefono.ToUpper()}/{concuantopagara}/{devuelta}/{direccion.ToUpper()}/{producto.ToUpper()}/{latitud}/{longitud}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<EUsuario> IniciarSesion(string email, string password)
        {

            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/IniciarSesion/{email}/{password}");
            var UsuarioResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EUsuario>(result);

            if (UsuarioResult.respuesta == "OK")
            {
                App.nombre = UsuarioResult.nombre;
                App.direccion = UsuarioResult.direccion;
                App.telefono = UsuarioResult.telefono;
                App.correo = UsuarioResult.correo;
                App.latitud = UsuarioResult.latitud;
                App.longitud = UsuarioResult.longitud;
                App.clave = UsuarioResult.clave;
            }

            return UsuarioResult;
        }

    }
}
