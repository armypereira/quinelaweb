using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QuinelaWeb.Models {
    public class Utilitario {

        public string GetAppSettingUsingConfigurationManager(string customField) {
            return System.Configuration.ConfigurationManager.AppSettings[customField];
        }

        public void SendMail(string subjet, string to, string valbody) {
            try {
                MailMessage mail = new MailMessage();
                SmtpClient server = new SmtpClient();
                string mailBody = string.Empty;
                string firma = string.Empty;

                mail.From = new MailAddress(GetAppSettingUsingConfigurationManager("CORREO"));
                server.Credentials = new System.Net.NetworkCredential(GetAppSettingUsingConfigurationManager("CORREO"), GetAppSettingUsingConfigurationManager("CLAVE"));
                server.Host = GetAppSettingUsingConfigurationManager("SERVIDOR");
                server.Port = Convert.ToInt32( GetAppSettingUsingConfigurationManager("PUERTO"));
                mail.To.Add(to);
                mail.Subject = subjet;
                mail.IsBodyHtml = true;
                mailBody = valbody;
                mail.Body = mailBody;
                server.EnableSsl = false;
                server.Send(mail);
               
            } catch (Exception ex) {
                
            }

            
        }
    }
}
