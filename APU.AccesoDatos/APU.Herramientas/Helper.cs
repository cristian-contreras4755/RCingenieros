using System;
using System.Web;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using APU.Entidad;
using Newtonsoft.Json;

namespace APU.Herramientas
{
    public static class Helper
    {
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
        public static string ObtenerTexto(string ruta)
        {
            var path = HttpContext.Current.Server.MapPath(ruta);
            return File.ReadAllText(path);
        }
        public static StringBuilder EncodeHtml(StringBuilder cadena)
        {
            cadena.Replace("á", "&aacute;");
            cadena.Replace("é", "&eacute;");
            cadena.Replace("í", "&iacute;");
            cadena.Replace("ó", "&oacute;");
            cadena.Replace("ú", "&uacute;");
            cadena.Replace("Á", "&Aacute;");
            cadena.Replace("É", "&Eacute;");
            cadena.Replace("Í", "&Iacute;");
            cadena.Replace("Ó", "&Oacute;");
            cadena.Replace("Ú", "&Uacute;");

            //cadena.Replace("à", "&agrave;");
            //cadena.Replace("è", "&eacute;");
            //cadena.Replace("ì", "&iacute;");
            //cadena.Replace("ò", "&oacute;");
            //cadena.Replace("ù", "&uacute;");
            //cadena.Replace("À", "&Aacute;");
            //cadena.Replace("È", "&Eacute;");
            //cadena.Replace("Ì", "&Iacute;");
            //cadena.Replace("Ò", "&Oacute;");
            //cadena.Replace("Ù", "&Uacute;");

            cadena.Replace("ñ", "&ntilde;");
            cadena.Replace("Ñ", "&Ntilde;");
            cadena.Replace("ä", "&auml;");
            cadena.Replace("ë", "&euml;");
            cadena.Replace("ï", "&iuml;");
            cadena.Replace("ö", "&ouml;");
            cadena.Replace("ü", "&uuml;");
            cadena.Replace("Ä", "&Auml;");
            cadena.Replace("Ë", "&Euml;");
            cadena.Replace("Ï", "&Iuml;");
            cadena.Replace("Ö", "&Ouml;");
            cadena.Replace("Ü", "&Uuml;");
            cadena.Replace("'", "&#39;");
            cadena.Replace("´", "&acute;");
            return cadena;
        }
        public static EmpresaSunatInfo ConsultaSunat(string ruc)
        {
            string str;
            HttpWebRequest request;
            HttpWebResponse response;
            Stream stream;
            StreamReader reader;

            str = string.Empty;
            request = (HttpWebRequest)WebRequest.Create("http://informaticaperuana.com/ukupacha/katariq.php?usr=iperuana&pwd=ip2017!&ruc=" + ruc);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (response = (HttpWebResponse)request.GetResponse())
            {
                using (stream = response.GetResponseStream())
                {
                    using (reader = new StreamReader(stream))
                    {
                        str = reader.ReadToEnd();
                    }
                }
            }
            var empresaSunatInfo = JsonConvert.DeserializeObject<EmpresaSunatInfo>(str);

            string[] strArray = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return empresaSunatInfo;
        }
        public static PersonaReniecInfo ConsultaReniec(string dni)
        {
            string str;
            HttpWebRequest request;
            HttpWebResponse response;
            Stream stream;
            StreamReader reader;

            str = string.Empty;
            request = (HttpWebRequest)WebRequest.Create("http://informaticaperuana.com/ukupacha/katariq.php?usr=iperuana&pwd=ip2017!&dni=" + dni);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (response = (HttpWebResponse)request.GetResponse())
            {
                using (stream = response.GetResponseStream())
                {
                    using (reader = new StreamReader(stream))
                    {
                        str = reader.ReadToEnd();
                    }
                }
            }
            var personaReniecInfo = new PersonaReniecInfo();
            if (!str.Equals("false"))
            {
                personaReniecInfo = JsonConvert.DeserializeObject<PersonaReniecInfo>(str);
            }
            

            // string[] strArray = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return personaReniecInfo;
        }
    }

    //public class EmpresaSunatInfo
    //{
    //    private string _razonSocial;
    //    public string RazonSocial
    //    {
    //        get { return _razonSocial; }
    //        set { _razonSocial = value; }
    //    }
    //    private string _estado;
    //    public string Estado
    //    {
    //        get { return _estado; }
    //        set { _estado = value; }
    //    }
    //    private string _direccion;
    //    public string Direccion
    //    {
    //        get { return _direccion; }
    //        set { _direccion = value; }
    //    }
    //}
    //public class PersonaReniecInfo
    //{
    //    private string _dni;
    //    public string Dni
    //    {
    //        get { return _dni; }
    //        set { _dni = value; }
    //    }
    //    private string _nombre;
    //    public string Nombre
    //    {
    //        get { return _nombre; }
    //        set { _nombre = value; }
    //    }
    //    private string _codigoVerificacion;
    //    public string CodigoVerificacion
    //    {
    //        get { return _codigoVerificacion; }
    //        set { _codigoVerificacion = value; }
    //    }
    //}
}