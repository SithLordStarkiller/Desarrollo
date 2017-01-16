using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;


namespace PubliPayments.Utiles
{
    public static class Email
    {
        private static readonly bool IsProduction = ConfigurationManager.AppSettings["Produccion"] != null && ConfigurationManager.AppSettings["Produccion"].ToLower() == "true";
        private static readonly string CorreoHost = ConfigurationManager.AppSettings["CorreoHost"].ToLower();
        private static readonly int CorreoPuerto = int.Parse(ConfigurationManager.AppSettings["CorreoPuerto"]);
        private static readonly bool CorreoSsl = bool.Parse(ConfigurationManager.AppSettings["CorreoSsl"]);
        public static Dictionary<string, string> Credenciales;
        public static void EnviarEmail(List<string> direccion, string subject, string body, bool esHtml = false)
        {
            if (!IsProduction) return;

            if (direccion.Count > 99)
                direccion = direccion.GetRange(0, 98);

            foreach (var dir in direccion)
            {
                EnviarEmail(dir, subject, body, esHtml);
            }
        }


        public static bool EnviarEmail(string direccion, string subject, string body, bool esHtml = false)
        {
            if (!IsProduction) return true;
            
            MailMessage mail = new MailMessage();
            foreach (var credencial in Credenciales)
            {
                try
                {
                    SmtpClient client;
                    string cuenta = credencial.Key;
                    string pass = credencial.Value;

                    client = new SmtpClient() { EnableSsl = CorreoSsl };
                    client.Host = CorreoHost;
                    client.Port = CorreoPuerto;

                    if (direccion != "")
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Email",
                            "EnviarEmail - " + subject + " - " + String.Join(",", direccion));
                        var fromAddress = new MailAddress(cuenta);
                        var smtpCredential = new System.Net.NetworkCredential(cuenta, pass);
                        direccion = direccion.ToLower().Trim();
                        
                        try
                        {
                            mail.To.Add(direccion);
                            client.UseDefaultCredentials = false;
                            client.Credentials = smtpCredential;
                            mail.From = new MailAddress(fromAddress.Address);
                            mail.Subject = subject;
                            mail.IsBodyHtml = esHtml;
                            Match m;
                            const string hRefPattern = "cid:(?<2>imagen\\d)\\s*{\\s*((?<1>[^}]*)[}])";
                            // se esta metiendo un } de mas XP 
                            try
                            {
                                m = Regex.Match(body, hRefPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                    TimeSpan.FromSeconds(1));
                                while (m.Success)
                                {

                                    var attachment =
                                        new Attachment(Directory.GetCurrentDirectory() + "\\" +
                                                       m.Groups[1].ToString().Replace("}", ""))
                                        {
                                            ContentId = m.Groups[2].ToString().Replace("}", "")
                                        };
                                    body = body.Replace("{" + m.Groups[1], "");
                                    mail.Attachments.Add(attachment);
                                    m = m.NextMatch();
                                }
                            }
                            catch (RegexMatchTimeoutException ex)
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Email",
                                    "EnviarEmail - Send - " + direccion + " - " + ex.Message);
                            }

                            mail.Body = body;
                            client.Send(mail);

                            return true;
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Email",
                                "EnviarEmail - Send - " + direccion + " - " + ex.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Email", "EnviarEmail - " + e.Message);
                }
            }
            return false;
        }
    }
}
