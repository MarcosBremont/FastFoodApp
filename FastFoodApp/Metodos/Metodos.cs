using FastFoodApp.Configuracion;
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

        //public async Task<List<EVerbos>> GetListadoVerbos()
        //{
        //    var result = await herramientas.EjecutarSentenciaEnApiLibre($"Verbs/ObtenerVerbos");
        //    var listado_verbos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EVerbos>>(result);

        //    return listado_verbos;
        //} // Fin del método ObtenerVerbos

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
