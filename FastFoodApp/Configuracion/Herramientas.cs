using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastFoodApp.Configuracion
{
    public class Herramientas
    {

        public Page defaultParentPage = null;


        internal string FromURLTOBase64(string fileName)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] response = wc.DownloadData(fileName);
                return ByteArrayToBase64(response);
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public List<string> SacarCadaCaracter(string TextoATrabajar)
        {
            List<string> MyCaracters = new List<string>();

            for (int i = 0; i < TextoATrabajar.Length; i++)
            {
                if (!MyCaracters.Contains(TextoATrabajar.Substring(i, 1)) && TextoATrabajar.Substring(i, 1) != "_")
                {
                    MyCaracters.Add(TextoATrabajar.Substring(i, 1));
                }
            }
            return MyCaracters;
        }



        public bool IsDecimalValido(string valor)
        {
            try
            {
                valor = valor.Replace("$", "");

                double s = Convert.ToDouble(valor);

                return true;
            }
            catch
            {
                if (string.IsNullOrEmpty(valor))
                    return true;
                else
                    return false;
            }
        }


        public string RepararMascara(string mask, string texto)
        {
            List<string> AllMyChars = SacarCadaCaracter(mask);

            for (int i = 0; i < texto.Length; i++)
            {
                if (!AllMyChars.Contains(texto.Substring(i, 1)) || texto.Substring(i, 1) == "_")
                {
                    //Mask sigue normal...
                }
                else
                {
                    if (texto.Substring(i, 1) != " ")
                    {
                        //Es un dígito o valor, significa que no está vacío...
                        return texto;
                    }
                }
            }

            return mask;
        }

        public bool IsValorEntero(string texto)
        {
            try
            {
                texto = texto.Replace("$", "");
                long isNumeric = Convert.ToInt64(texto);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int StrToInt(string numero)
        {
            int numero_int;
            int.TryParse(numero, out numero_int);
            return numero_int;
        }


        //public ImageSource Base64ToImageSource(string base64)
        //{
        //    try
        //    {
        //        byte[] Base64Stream = Convert.FromBase64String(base64);
        //        return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        //    }
        //    catch (Exception)
        //    {
        //        byte[] Base64Stream = Convert.FromBase64String(Configuracion.NoImageFound);
        //        return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        //    }
        //}



        public byte[] ImageToByteArray(string imgSourcePath)
        {
            //file to base64 string
            return System.IO.File.ReadAllBytes(imgSourcePath);
        }

        public string ByteArrayToBase64(byte[] ByteArray)
        {
            return Convert.ToBase64String(ByteArray);
        }

        public string PathToBase64(string PathFileFrom)
        {
            if (PathFileFrom is null)
                return "Image not found...";
            return ByteArrayToBase64(ImageToByteArray(PathFileFrom));
        }

        public async Task<bool> MsgPregunta(Page pageSender, string msg, string buttonOkName = "ACEPTAR", string buttonCancelName = "CANCELAR", string title = "")
        {
            bool myResult = false;

            //Make the question...
            var result = await pageSender.DisplayAlert(title, msg, buttonOkName, buttonCancelName);
            if (result)
                myResult = true;
            else
                myResult = false;

            //Retrieve the answer...
            return myResult;
        }



        public async Task<string> EjecutarSentenciaEnApiLibre(string urlPart, bool check = false)
        {
            string request = "";
            try
            {
                string url = Configuracion.ServerApi + urlPart;

                HttpClient clientHTTP = new HttpClient();
                var response = await clientHTTP.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    request = jsonString.ToString();
                }

            }
            catch (Exception ex)
            {
                //UserDialogs.Instance.Toast("¡No se pudo establecer conexión con el servidor!. ");
            }
            return request;
        }


        public async Task<string> EjecutarMetodoPost(string parametros, string body_data)
        {
            try
            {
                string url = Configuracion.ServerApi + parametros;
                /*                string bvody_data_formated = "\"" + body_data.Replace("\"", "\\" + "\"") + "\""*/
                ;

                HttpClient client = new HttpClient();
                var content = new StringContent(body_data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString.ToString();
            }
            catch (Exception ex)
            {
                //UserDialogs.Instance.Toast("¡No se pudo establecer conexión con el servidor!. ");
                throw ex;
            }
        }

        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

    }
}
