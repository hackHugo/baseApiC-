using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApplication2.Utilities
{
    public class Mail
    {
        string From = "Incubar";
        string To;
        string Message;
        string Subject;
        List<string> Archivo = new List<string>();
        string DE = "";
        string PASS = "";

        System.Net.Mail.MailMessage Email;

        public string error = "";
        public string mensaje = "";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="FROM">Procedencia</param>
        /// <param name="Para">Mail al cual se enviara</param>
        /// <param name="Mensaje">Mensaje del mail</param>
        /// <param name="Asunto">Asunto del mail</param>
        /// <param name="ArchivoPedido_">Archivo a adjuntar, no es obligatorio</param>
        public Mail(string FROM, string Para, string Mensaje, string Asunto, List<string> ArchivoPedido_ = null)
        {
            From = FROM;
            To = Para;
            Message = Mensaje;
            Subject = Asunto;
            Archivo = ArchivoPedido_;

            DE = ConfigurationManager.AppSettings["userSMTP"];
            PASS = ConfigurationManager.AppSettings["passSMTP"];
        }

        /// <summary>
        /// metodo que envia el mail
        /// </summary>
        /// <returns></returns>
        public bool enviaMail()
        {
            bool exito = false;

            if (To.Trim().Equals("") || Message.Trim().Equals("") || Subject.Trim().Equals(""))
            {
                error = "El mail, el asunto y el mensaje son obligatorios";
                return false;
            }

            /* if (!ExpresionesRegulares.RegEX.isEmail(To))
             {
                 error = "El mail [" + To + "] no es un mail valido";
                 return false;
             }*/
            //comienza-------------------------------------------------------------------------
            try
            {
                Email = new System.Net.Mail.MailMessage(From, To, Subject, Message);

                //si viene archivo a adjuntar
                if (Archivo != null)
                {
                    //agregado de archivo
                    foreach (string archivo in Archivo)
                    {
                        if (System.IO.File.Exists(@archivo))
                            Email.Attachments.Add(new Attachment(@archivo));

                    }
                }
                System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["serverSMTP"]);
                Email.IsBodyHtml = true;
                Email.From = new MailAddress(From);


                smtpMail.EnableSsl = false;
                smtpMail.UseDefaultCredentials = false;
                smtpMail.Host = ConfigurationManager.AppSettings["serverSMTP"];
                smtpMail.Port = int.Parse(ConfigurationManager.AppSettings["portSMTP"]);
                smtpMail.Credentials = new System.Net.NetworkCredential(DE, PASS);

                smtpMail.Send(Email);
                smtpMail.Dispose();
                mensaje = "Se envio el mail con exito";
                exito = true;
            }
            catch (Exception ex)
            {
                error = "Ocurrio un error: " + ex.Message;
                exito = false;
            }

            return exito;

        }
    }
}