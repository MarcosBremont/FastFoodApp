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
        } // Fin del método ObtenerVerbos

        //public async Task<List<EPosiciones>> GetListadoDePosiciones()
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/Posiciones");
        //    var listado_Posiciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EPosiciones>>(result);

        //    return listado_Posiciones;
        //} // Fin del método ObtenerTablaDePosiciones



        public async Task<Result> EnterToTheTournament(string nombrePersona, int numeroVerbosCorrectos, string direccion)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/EnterToTheTournament/{nombrePersona.ToUpper()}/{numeroVerbosCorrectos}/{direccion.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }

        public async Task<Result> SendEmails(string email)
        {
            var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/SaveEmailNews/{email.ToUpper()}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(result);
            return response;
        }
    }
}
