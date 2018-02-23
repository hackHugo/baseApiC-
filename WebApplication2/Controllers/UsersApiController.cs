using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models.Response;
using WebApplication2.Models;
using WebApplication2.Models.Request;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web;
using System.Configuration;

namespace WebApplication2.Controllers
{
    
    public class UsersApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Get data</remarks>
        /// <response code="200">respuesta correcta</returns>
        /// <response code="400">Error en request</returns>
        /// <response code="500">Error de server</returns>
        [ResponseType(typeof(ReplyUser))]
        public IHttpActionResult login(UserRequest oRequest)
        {
            ReplyUser oReply = new ReplyUser();
            oReply.status = 1;
            try
            {
                if (!ModelState.IsValid)
                {
                    oReply.message = GetErrorsModel();
                    return BadRequest(oReply.message);
                }
                string Token = Guid.NewGuid().ToString();
                string encryptPass = Utilities.Encrypt.GetSHA256(oRequest.email);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(oReply);
        }
        /// <summary>
        /// Envia email para recuperar contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Api/Admin/Access/EmailRecover")]
        [HttpPost]
        public Reply emailRecover(EmailRecoverRequest oRequest)
        {
            Reply oReply = new Reply();
            oReply.status = Constants.ERROR;
            try
            {
                //validaciones comunes
                if (!ModelState.IsValid)
                {
                    oReply.message = GetErrorsModel();
                    return oReply;
                }
                using (ZonasAkronDevEntities db = new ZonasAkronDevEntities())
                {
                    users oUser = (from d in db.users
                                   where d.email.Equals(oRequest.email) && d.idStatus == 1
                                   select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        oReply.message = "El correo electrónico no está registrado en el sistema";
                    }
                    else
                    {
                        string token = Utilities.Encrypt.GetSHA256(oRequest.email + "" + DateTime.Now);
                        //actualizamos token
                        if (sendEmail(oRequest.email, token) != false)
                        {
                            oUser.resetPassword = token;
                            oUser.updated_at = DateTime.Now;
                            db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            oReply.status = Constants.SUCCESS;
                            oReply.message = "Tu correo se ha enviado";
                        }
                        else
                        {
                            oReply.message = "Ocurrio un error al enviar el email de autorización.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oReply.message = Constants.Exception.ERROR + ex.Message;
            }
            return oReply;
        }
        /// <summary>
        /// Autentifica a un usuario por medio de su contrasña y usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Api/Admin/Access/ResetPassword")]
        [HttpPost]
        public Reply resetPassword(ResetPasswordRequest oRequest)
        {
            Reply oReply = new Reply();
            oReply.status = Constants.ERROR;
            try
            {
                //validaciones comunes
                if (!ModelState.IsValid)
                {
                    oReply.message = GetErrorsModel();
                    return oReply;
                }
                using (ZonasAkronDevEntities db = new ZonasAkronDevEntities())
                {
                    users oUser = (from d in db.users
                                   where d.resetPassword.Equals(oRequest.token) && d.idStatus == 1
                                   select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        oReply.message = "Su solicitud ya no es válida inténtelo nuevamente.";
                    }
                    else
                    {
                        string newPass = Utilities.Encrypt.GetSHA256(oRequest.password);
                        oUser.password = newPass;
                        oUser.resetPassword = "";
                        oUser.updated_at = DateTime.Now;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        oReply.status = Constants.SUCCESS;
                        oReply.message = "Tu contraseña se modificó correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                oReply.message = Constants.Exception.ERROR + ex.Message;
            }
            return oReply;
        }
        [Route("Api/Admin/Access/SignOff")]
        [HttpGet]
        public Reply signOff()
        {
            Reply oReply = new Reply();
            oReply.status = Constants.ERROR;
            try
            {
                var token = "";
                if (Request.Headers.Authorization != null)
                {
                    token = Request.Headers.Authorization.ToString();
                }
                using (ZonasAkronDevEntities db = new ZonasAkronDevEntities())
                {
                    users oUser = (from x in db.users
                                   where x.token == token && x.idStatus == 1
                                   select x).FirstOrDefault();
                    if (oUser == null)
                    {
                        oReply.message = "El usuario no existe";
                    }
                    else
                    {
                        oUser.token = "";
                        oUser.updated_at = DateTime.Now;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        oReply.status = Constants.SUCCESS;
                        oReply.message = "Tu sesión ha sido cerrada";
                    }

                }
            }
            catch (Exception ex)
            {
                oReply.message = Constants.Exception.ERROR + ex.Message;
            }
            return oReply;
        }
        #region HELPERS
        //metodo para enviar el correo devuelve un true o false 
        private Boolean sendEmail(string oRequestEmail, string token)
        {
            //se define path
            string path = HttpContext.Current.Server.MapPath("~");
            //trae archivo HTML
            string mensaje = Utilities.Archivo.GetStringOfFile(path + "Views/Mails/RecuperarPassword.html");

            string urlAccion = ConfigurationManager.AppSettings["domainNameResetPassword"].ToString() + "?token=" + token;
            string urlImg = ConfigurationManager.AppSettings["domainName"].ToString() + "";
            //remplazo
            mensaje = mensaje.Replace("@Fecha@", DateTime.Now.ToString());
            mensaje = mensaje.Replace("@Compania@", "Zonas Akron");
            mensaje = mensaje.Replace("@UrlAccion@", urlAccion);
            mensaje = mensaje.Replace("@urlImg@", urlImg);
            //Parametros para enviar email
            string From = ConfigurationManager.AppSettings["mailContacto"];
            string Para = oRequestEmail;
            string Mensaje = mensaje;
            string Asunto = "Recuperar contraseña";

            //se envia email
            var email = new Utilities.Mail(From, Para, Mensaje, Asunto);

            // valida si se envia email
            if (!email.enviaMail())
            {
                return false;
            }
            return true;
        }
        private string GetErrorsModel()
        {
            string errors = "";
            foreach (ModelState modelState in ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + "\n";
                }
            }

            return errors;
        }
        #endregion

    }
}
