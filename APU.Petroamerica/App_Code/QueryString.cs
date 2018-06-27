using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace APU.Petroamerica
{
    public class QueryString: IHttpModule
    {
        private const string nombreParametro = "q=";
        private const string llaveEncriptacion = "key";

        private static readonly byte[] salt = Encoding.ASCII.GetBytes(llaveEncriptacion.Length.ToString());

        public QueryString()
        {
            
        }
        public void Dispose()
        {
            
        }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
            {
                string query = ExtraerCadena(context.Request.Url.AbsoluteUri);
                string ruta = ObtenerRutaVirtual();

                if (query.StartsWith(nombreParametro, StringComparison.OrdinalIgnoreCase))
                {
                    string rawQuery = query.Replace(nombreParametro, String.Empty);
                    string decryptedQuery = Desencriptar(rawQuery);
                    context.RewritePath(ruta, String.Empty, decryptedQuery);
                }
                else
                {
                    if (context.Request.HttpMethod.Equals("GET"))
                    {
                        string encryptedQuery = Encriptar(query);
                        context.Response.Redirect(ruta + encryptedQuery);
                    }
                }
            }
        }

        private static string ObtenerRutaVirtual()
        {
            string ruta = HttpContext.Current.Request.RawUrl;
            ruta = ruta.Substring(0, ruta.IndexOf("?"));
            ruta = ruta.Substring(ruta.LastIndexOf("/") + 1);
            return ruta;
        }

        private static string ExtraerCadena(string url)
        {
            int indice = url.IndexOf("?") + 1;
            return url.Substring(indice);
        }

        public static string Encriptar(string cadenaEntrada)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            byte[] textoPlano = Encoding.Unicode.GetBytes(cadenaEntrada);
            PasswordDeriveBytes llave = new PasswordDeriveBytes(llaveEncriptacion, salt);
            using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(llave.GetBytes(32), llave.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(textoPlano, 0, textoPlano.Length);
                        cryptoStream.FlushFinalBlock();
                        return "?" + nombreParametro + Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Desencriptar(string cadenaEntrada)
        {
            try
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                byte[] datosCifrados = Convert.FromBase64String(cadenaEntrada);
                PasswordDeriveBytes llave = new PasswordDeriveBytes(llaveEncriptacion, salt);
                using (
                    ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(llave.GetBytes(32), llave.GetBytes(16))
                    )
                {
                    using (MemoryStream memoryStream = new MemoryStream(datosCifrados))
                    {
                        using (
                            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                            )
                        {
                            byte[] textoPlano = new byte[datosCifrados.Length];
                            int contador = cryptoStream.Read(textoPlano, 0, textoPlano.Length);
                            return Encoding.Unicode.GetString(textoPlano, 0, contador);
                        }
                    }
                }
            }
            catch (FormatException)
            {
                HttpContext context = HttpContext.Current;
                //context.Response.Redirect(");
                return null;
            }
            catch (CryptographicException)
            {
                HttpContext context = HttpContext.Current;
                //context.Response.Redirect(");
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                HttpContext context = HttpContext.Current;
                //context.Response.Redirect(");
                return null;
            }
            finally
            {
                
            }
        }
    }
}