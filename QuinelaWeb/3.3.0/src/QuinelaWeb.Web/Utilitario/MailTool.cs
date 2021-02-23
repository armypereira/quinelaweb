using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace QuinelaWeb.Web.Utilitario {
    public class MailTool {



        public string GetAppSettingUsingConfigurationManager(string customField) {
            return System.Configuration.ConfigurationManager.AppSettings[customField];
        }
        public void SendMailRegistro(string subjet, string to, string Usuario, string Clave) {


        }

        
    

        public void SendMail(string valSubjet, string valTo, string valBody) {
            try {
                MailMessage mail = new MailMessage();
                SmtpClient server = new SmtpClient();
                string mailBody = string.Empty;
                string firma = string.Empty;
                mail.From = new MailAddress(GetAppSettingUsingConfigurationManager("CORREO"));
                server.Credentials = new System.Net.NetworkCredential(GetAppSettingUsingConfigurationManager("CORREO"), GetAppSettingUsingConfigurationManager("CLAVE"));
                server.Host = GetAppSettingUsingConfigurationManager("SERVIDOR");
                server.Port = Convert.ToInt32(GetAppSettingUsingConfigurationManager("PUERTO"));
                mail.To.Add(valTo);
                mail.Subject = valSubjet;
                mail.IsBodyHtml = true;
                mailBody = valBody;
                mail.Body = mailBody;
                server.EnableSsl = true;
                server.Send(mail);

            } catch (Exception ex) {

            }


        }
    }
}