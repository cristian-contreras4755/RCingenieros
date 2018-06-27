using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web;

namespace APU.Negocio
{
    public static class Email
    {
        #region Variables
        private static string displayName = ConfigurationManager.AppSettings["APU.DisplayName"];
        private static string fromEmail = ConfigurationManager.AppSettings["APU.FromEmail"];
        private static string passwordEmail = ConfigurationManager.AppSettings["APU.PasswordEmail"];
        private static string serverEmail = ConfigurationManager.AppSettings["APU.ServerEmail"];
        private static string copiaEmail = ConfigurationManager.AppSettings["SMART.CopiaEmail"];
        private static string copiaOcultaDesarrollo = ConfigurationManager.AppSettings["SMART.CopiaOculta"];
        #endregion

        public static void Enviar(string para, string conCopia, string copiaOculta, string asunto, string mensaje, bool esHTML)
        {
            try
            {
                var message = new MailMessage();
                var server = new SmtpClient();

                #region Para
                para = para.Trim(';');
                if (para.Length > 0)
                {
                    foreach (var address in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia
                conCopia = conCopia.Trim(';');
                if (conCopia.Length > 0)
                {
                    foreach (var address in conCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.CC.Add(address);
                    }
                }
                #endregion

                #region Con Copia Oculta
                copiaOculta = copiaOculta.Trim(';');
                if (copiaOculta.Length > 0)
                {
                    foreach (var address in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.Bcc.Add(address);
                    }
                }
                #endregion

                message.IsBodyHtml = true;
                message.Body = mensaje;
                message.From = new MailAddress(fromEmail, displayName);
                server.Send(message);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }
        public static void Enviar(string para, string conCopia, string copiaOculta, string asunto, string mensaje, string adjunto)
        {
            try
            {
                // first we create a plain text version and set it to the AlternateView
                // then we create the HTML version
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;

                #region Para
                para = para.Trim(';');
                if (para.Length > 0)
                {
                    foreach (var address in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia
                conCopia = conCopia.Trim(';');
                if (conCopia.Length > 0)
                {
                    foreach (var address in conCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.CC.Add(address);
                    }
                }
                #endregion

                #region Con Copia Oculta
                copiaOculta = copiaOculta.Trim(';');
                if (copiaOculta.Length > 0)
                {
                    foreach (var address in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.Bcc.Add(address);
                    }
                }
                #endregion

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (!adjunto.Equals(String.Empty))
                {
                    var attach = new Attachment(adjunto);
                    msg.Attachments.Add(attach);
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                //example of a linked image
                //var imgLogo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Correos/Imagenes/FirmaBcp.jpeg"), System.Net.Mime.MediaTypeNames.Image.Jpeg);
                //var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                //var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                //imgLogo.ContentId = "@FIRMA";
                //imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                //imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //htmlView.LinkedResources.Add(imgLogo);

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                smtp.Send(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }

        public static void Enviar(List<string> correoPara, List<string> CorreoConCopia, List<string> CorreoCopiaOculta, string asunto, string mensaje, string adjunto)
        {
            try
            {
                // first we create a plain text version and set it to the AlternateView
                // then we create the HTML version
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;
                foreach (var correo in correoPara)
                {
                    msg.To.Add(correo);
                }

                //foreach (var correo in CorreoConCopia)
                //{
                //    msg.CC.Add(correo);
                //}
                msg.CC.Add(copiaEmail);

                //foreach (var correo in CorreoCopiaOculta)
                //{
                //    msg.Bcc.Add(correo);
                //}

                msg.Bcc.Add(copiaOcultaDesarrollo);

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (!adjunto.Equals(String.Empty))
                {
                    var attach = new Attachment(adjunto);
                    msg.Attachments.Add(attach);
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                //var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                //var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                //imgLogo.ContentId = "@FIRMA";
                //imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                //imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //htmlView.LinkedResources.Add(imgLogo);

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                smtp.Send(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }

        public static void Enviar(string para, string conCopia, string copiaOculta, string asunto, string mensaje, List<string> adjuntos)
        {
            try
            {
                // first we create a plain text version and set it to the AlternateView
                // then we create the HTML version
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;

                #region Para
                para = para.Trim(';');
                if (para.Length > 0)
                {
                    foreach (var address in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia
                conCopia = conCopia.Trim(';');
                if (conCopia.Length > 0)
                {
                    foreach (var address in conCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Copia Oculta
                copiaOculta = copiaOculta.Trim(';');
                if (copiaOculta.Length > 0)
                {
                    foreach (var address in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.Bcc.Add(address);
                    }
                }
                #endregion

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (adjuntos != null && adjuntos.Count() > 0)
                {
                    foreach (var fila in adjuntos)
                    {
                        var attach = new Attachment(fila);
                        msg.Attachments.Add(attach);
                    }
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                //example of a linked image
                var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLogo.ContentId = "@FIRMA";
                imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLogo);

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                smtp.Send(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }

        public static void EnviarDesembolso(string para, string conCopia, string copiaOculta, string asunto, string mensaje, List<string> adjuntos)
        {
            try
            {
                // first we create a plain text version and set it to the AlternateView
                // then we create the HTML version
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;

                #region Para
                para = para.Trim(';');
                if (para.Length > 0)
                {
                    foreach (var address in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia
                conCopia = conCopia.Trim(';');
                if (conCopia.Length > 0)
                {
                    foreach (var address in conCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.CC.Add(address);
                    }
                }
                #endregion

                #region Con Copia Oculta
                copiaOculta = copiaOculta.Trim(';');
                if (copiaOculta.Length > 0)
                {
                    foreach (var address in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.Bcc.Add(address);
                    }
                }
                #endregion

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (adjuntos != null && adjuntos.Count() > 0)
                {
                    foreach (var fila in adjuntos)
                    {
                        var attach = new Attachment(fila);
                        msg.Attachments.Add(attach);
                    }
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                //example of a linked image
                var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLogo.ContentId = "@FIRMA";
                imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLogo);

                //msg.AlternateViews.Add(htmlView);

                #region Linkear Imagenes

                var rutaImgOpDes = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "imagenbcp_opdesembolsada.jpg";
                var imgOpDes = new LinkedResource(rutaImgOpDes, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgOpDes.ContentId = "@ENCABEZADO_CORREO";
                imgOpDes.ContentType.Name = "imagenbcp_opdesembolsada.jpg";
                imgOpDes.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgOpDes);

                //msg.AlternateViews.Add(htmlView);

                var rutaImgLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "logobcp_correo.jpg";
                var imgLog = new LinkedResource(rutaImgLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLog.ContentId = "@ENCABEZADO";
                imgLog.ContentType.Name = "logobcp_correo.jpg";
                imgLog.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLog);

                //msg.AlternateViews.Add(htmlView);

                var rutaImgPie = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "imagenbcp_opdesembolsada_pie.jpg";
                var imgPie = new LinkedResource(rutaImgPie, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgPie.ContentId = "@PIE_CORREO";
                imgPie.ContentType.Name = "imagenbcp_opdesembolsada_pie.jpg";
                imgPie.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgPie);

                //msg.AlternateViews.Add(htmlView);

                #endregion

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                smtp.Send(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }
        public static void EnviarDesembolso(List<string> correoPara, List<string> CorreoConCopia, List<string> CorreoCopiaOculta, string asunto, string mensaje, List<string> adjuntos)
        {
            try
            {
                // first we create a plain text version and set it to the AlternateView
                // then we create the HTML version
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;

                foreach (var correo in correoPara)
                {
                    msg.To.Add(correo);
                }

                foreach (var correo in CorreoConCopia)
                {
                    msg.CC.Add(correo);
                }

                foreach (var correo in CorreoCopiaOculta)
                {
                    msg.Bcc.Add(correo);
                }
                //msg.Bcc.Add(copiaOcultaDesarrollo);

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (adjuntos != null && adjuntos.Count() > 0)
                {
                    foreach (var fila in adjuntos)
                    {
                        var attach = new Attachment(fila);
                        msg.Attachments.Add(attach);
                    }
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                //example of a linked image
                var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLogo.ContentId = "@FIRMA";
                imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLogo);

                //msg.AlternateViews.Add(htmlView);

                #region Linkear Imagenes

                var rutaImgOpDes = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "imagenbcp_opdesembolsada.jpg";
                var imgOpDes = new LinkedResource(rutaImgOpDes, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgOpDes.ContentId = "@ENCABEZADO_CORREO";
                imgOpDes.ContentType.Name = "imagenbcp_opdesembolsada.jpg";
                imgOpDes.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgOpDes);

                //msg.AlternateViews.Add(htmlView);

                var rutaImgLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "logobcp_correo.jpg";
                var imgLog = new LinkedResource(rutaImgLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLog.ContentId = "@ENCABEZADO";
                imgLog.ContentType.Name = "logobcp_correo.jpg";
                imgLog.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLog);

                //msg.AlternateViews.Add(htmlView);

                var rutaImgPie = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "imagenbcp_opdesembolsada_pie.jpg";
                var imgPie = new LinkedResource(rutaImgPie, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgPie.ContentId = "@PIE_CORREO";
                imgPie.ContentType.Name = "imagenbcp_opdesembolsada_pie.jpg";
                imgPie.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgPie);

                //msg.AlternateViews.Add(htmlView);

                #endregion

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                smtp.Send(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }
        public static void EnviarAsincrono(string para, string conCopia, string copiaOculta, string asunto, string mensaje, string adjunto)
        {
            try
            {
                var msg = new MailMessage();

                msg.From = new MailAddress(fromEmail, displayName);
                msg.Subject = asunto;

                #region Para
                para = para.Trim(';');
                if (para.Length > 0)
                {
                    foreach (var address in para.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia
                conCopia = conCopia.Trim(';');
                if (conCopia.Length > 0)
                {
                    foreach (var address in conCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(address);
                    }
                }
                #endregion

                #region Con Copia Oculta
                copiaOculta = copiaOculta.Trim(';');
                if (copiaOculta.Length > 0)
                {
                    foreach (var address in copiaOculta.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.Bcc.Add(address);
                    }
                }
                #endregion

                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Body = mensaje;

                if (!adjunto.Equals(String.Empty))
                {
                    var attach = new Attachment(adjunto);
                    msg.Attachments.Add(attach);
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensaje, Encoding.UTF8, "text/html");

                var rutaLogo = ConfigurationManager.AppSettings["SMART.Archivos.Correos.Imagenes"] + "\\" + "FirmaBcp.jpeg";
                var imgLogo = new LinkedResource(rutaLogo, System.Net.Mime.MediaTypeNames.Image.Jpeg);
                imgLogo.ContentId = "@FIRMA";
                imgLogo.ContentType.Name = "FirmaBcp.jpeg";
                imgLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imgLogo);

                msg.AlternateViews.Add(htmlView);

                //sending prepared email
                var smtp = new SmtpClient();//It reads the SMPT params from Web.config
                smtp.Credentials = new NetworkCredential(fromEmail, passwordEmail);
                //smtp.Port = 465;
                smtp.Host = serverEmail;

                // smtp.Send(msg);
                smtp.SendMailAsync(msg);
            }
            catch (SmtpException ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
            }
        }
        public static string ObtenerMensaje(string ruta)
        {
            string path = HttpContext.Current.Server.MapPath(ruta);
            return File.ReadAllText(path);
        }

        //public static void EnviarCorreosAutomatico(OperacionInfo operacionInfo, ClienteInfo cliente, string documentos, int estadoOperacion, string asunto, string mensaje, string adjunto)
        //{
        //    var matrizCorreo = new Negocio.MatrizCorreo();
        //    var listaCorreos = matrizCorreo.ListarMatrizCorreos(operacionInfo.ModeloNegocioId, documentos, estadoOperacion);

        //    if (listaCorreos.Count == 0) return;

        //    List<string> correoPara;
        //    List<string> correoCopia;
        //    List<string> correoCopiaOculta;

        //    correoPara = Negocio.MatrizCorreo.ObtenerCorreosDestinatarios(listaCorreos, operacionInfo, cliente, out correoCopia, out correoCopiaOculta);

        //    Negocio.Email.Enviar(correoPara, correoCopia, correoCopiaOculta, asunto, mensaje, adjunto);
        //}
        //public static void EnviarCorreosAutomaticoDesembolso(OperacionInfo operacionInfo, ClienteInfo cliente, string documentos, int estadoOperacion, string asunto, string mensaje, List<string> adjunto)
        //{
        //    var matrizCorreo = new Negocio.MatrizCorreo();
        //    var listaCorreos = matrizCorreo.ListarMatrizCorreos(operacionInfo.ModeloNegocioId, documentos, estadoOperacion);

        //    if (listaCorreos.Count == 0) return;

        //    List<string> correoPara;
        //    List<string> correoCopia;
        //    List<string> correoCopiaOculta;

        //    correoPara = Negocio.MatrizCorreo.ObtenerCorreosDestinatarios(listaCorreos, operacionInfo, cliente, out correoCopia, out correoCopiaOculta);

        //    Negocio.Email.EnviarDesembolso(correoPara, correoCopia, correoCopiaOculta, asunto, mensaje, adjunto);
        //}

        //public static void EnviarCorreosAutomaticoMasivo(OperacionInfo operacionInfo, ClienteInfo cliente, string documentos, int estadoOperacion, string asunto, string mensaje, string adjunto)
        //{
        //    var matrizCorreo = new Negocio.MatrizCorreo();
        //    var listaCorreos = matrizCorreo.ListarMatrizCorreos(operacionInfo.ModeloNegocioId, documentos, estadoOperacion, 0, 1);

        //    if (listaCorreos.Count == 0) return;

        //    List<string> correoPara;
        //    List<string> correoCopia;
        //    List<string> correoCopiaOculta;

        //    correoPara = Negocio.MatrizCorreo.ObtenerCorreosDestinatarios(listaCorreos, operacionInfo, cliente, out correoCopia, out correoCopiaOculta);

        //    Negocio.Email.Enviar(correoPara, correoCopia, correoCopiaOculta, asunto, mensaje, adjunto);
        //}

        //public static void EnviarCorreosAutomaticoExcepcion(OperacionInfo operacionInfo, int tipoExcepcionId, int areaDerivacionId, int estadoId, string asunto, string mensaje, string adjunto)
        //{
            //var matrizCorreo = new Negocio.MatrizCorreoExcepcion();
            //var modeloNegocioId = operacionInfo.ModeloNegocioId;
            //if (estadoId.Equals(Constantes.EstadoExcepcionAceptada) || estadoId.Equals(Constantes.EstadoExcepcionDerivada))
            //{
            //    modeloNegocioId = 0;
            //}
            //var listaCorreos = matrizCorreo.ListarMatrizCorreoExcepcion(tipoExcepcionId, areaDerivacionId, estadoId, modeloNegocioId);

            //if (listaCorreos.Count == 0) return;

            //List<string> correoPara;
            //List<string> correoCopia;
            //List<string> correoCopiaOculta;

            //correoPara = Negocio.MatrizCorreoExcepcion.ObtenerCorreosDestinatarios(listaCorreos, operacionInfo, out correoCopia, out correoCopiaOculta);

            //Negocio.Email.Enviar(correoPara, correoCopia, correoCopiaOculta, asunto, mensaje, adjunto);
        //}
    }
}