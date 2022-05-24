﻿using FastFoodApp.Configuracion;
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



        public async Task<Result> AgregarPedido(string usuario, string email, string telefono, int concuantopagara, int devuelta, string direccion, StringBuilder producto, string latitud, string longitud)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/AgregarPedido/{usuario.ToUpper()}/{email.ToUpper()}/{telefono.ToUpper()}/{concuantopagara}/{devuelta}/{direccion.ToUpper()}/{producto}/{latitud}/{longitud}");
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
                App.apellido = UsuarioResult.apellido;
                App.direccion = UsuarioResult.direccion;
                App.telefono = UsuarioResult.telefono;
                App.correo = UsuarioResult.correo;
                App.latitud = UsuarioResult.latitud;
                App.longitud = UsuarioResult.longitud;
                App.clave = UsuarioResult.clave;
            }

            return UsuarioResult;
        }

        public async Task<List<EPedidos>> ObtenerPedidos()
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerPedidos");
            var lista_pedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPedidos>>(result);

            return lista_pedidos;
        } // Fin del método ObtenerMenu

        public async Task<Result> RegistrarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string latitud, string longitud, string clave)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/RegistratUsuario/{nombre.ToUpper()}/{apellido.ToUpper()}/{direccion.ToUpper()}/{telefono}/{email.ToUpper()}/{direccion.ToUpper()}/{latitud}/{latitud}/{longitud}/{clave.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }// Fin del método ObtenerEmpresa


        public async Task<List<EPedidos>> ObtenerCarritoPorUsuario(int idusuario, int idpedidos)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"FastFood/ObtenerCarritoPorUsuario/{idpedidos}/{idusuario}");
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


    }
}
