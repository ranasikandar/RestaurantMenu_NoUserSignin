using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RestaurantMenu.Helpers
{
    public class Email
    {
        Email() { }

        public static bool Send(string FromEmail, string FromName, string ToEmail, string ToName, string Subject, string Body, bool IsBodyHtml, Attachment attach, HttpFileCollection files = null)
        {
            bool Success = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage Message = new MailMessage();

            try
            {

                if (string.IsNullOrEmpty(ToName) || ToEmail.Contains(","))
                    Message.To.Add(ToEmail);
                else
                    Message.To.Add(new MailAddress(ToEmail, ToName));

                if (!string.IsNullOrEmpty(FromEmail))
                {
                    if (string.IsNullOrEmpty(FromName))
                        Message.From = new MailAddress(FromEmail);
                    else
                        Message.From = new MailAddress(FromEmail, FromName);
                }


                if (attach != null)
                    Message.Attachments.Add(attach);

                if (files != null)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile uploadfile = files[i];

                        if (uploadfile.ContentLength > 0)
                            Message.Attachments.Add(new Attachment(uploadfile.InputStream, Path.GetFileName(uploadfile.FileName)));
                    }
                }

                //smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 100000;

                Message.Subject = Subject;
                Message.Body = Body;
                Message.IsBodyHtml = IsBodyHtml;

                smtpClient.Send(Message);

                Success = true;

                if (attach != null)
                    attach.Dispose();
                smtpClient.Dispose();
                Message.Dispose();
            }
            catch (Exception ex)
            {
                if (attach != null)
                    attach.Dispose();
                smtpClient.Dispose();
                Message.Dispose();

                Helpers.WriteLogToFile.Write("Error sending email - Try #1 - (Email.cs file) - " + ex.Message + " To: " + ToEmail + " Body: " + Body);

                System.Threading.Thread.Sleep(1000);

                return SendTry2(FromEmail, FromName, ToEmail, ToName, Subject, Body, IsBodyHtml, attach, files);
            }

            return Success;
        }

        private static bool SendTry2(string FromEmail, string FromName, string ToEmail, string ToName, string Subject, string Body, bool IsBodyHtml, Attachment attach, HttpFileCollection files = null)
        {
            bool Success = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage Message = new MailMessage();

            try
            {

                if (string.IsNullOrEmpty(ToName) || ToEmail.Contains(","))
                    Message.To.Add(ToEmail);
                else
                    Message.To.Add(new MailAddress(ToEmail, ToName));

                if (!string.IsNullOrEmpty(FromEmail))
                {
                    if (string.IsNullOrEmpty(FromName))
                        Message.From = new MailAddress(FromEmail);
                    else
                        Message.From = new MailAddress(FromEmail, FromName);
                }


                if (attach != null)
                    Message.Attachments.Add(attach);

                if (files != null)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile uploadfile = files[i];

                        if (uploadfile.ContentLength > 0)
                            Message.Attachments.Add(new Attachment(uploadfile.InputStream, Path.GetFileName(uploadfile.FileName)));
                    }
                }

                //smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 100000;

                Message.Subject = Subject;
                Message.Body = Body;
                Message.IsBodyHtml = IsBodyHtml;

                smtpClient.Send(Message);

                Success = true;

                if (attach != null)
                    attach.Dispose();
                smtpClient.Dispose();
                Message.Dispose();
            }
            catch (Exception ex)
            {
                if (attach != null)
                    attach.Dispose();
                smtpClient.Dispose();
                Message.Dispose();

                Helpers.WriteLogToFile.Write("Error sending email - Try #2 - (Email.cs file) - " + ex.Message + " To: " + ToEmail + " Body: " + Body);
                System.Threading.Thread.Sleep(1000);
                return false;
            }

            return Success;
        }

        private static string Get_Template(string Name)
        {
            string file_name = System.IO.Path.GetFullPath(HttpContext.Current.Server.MapPath("~/Assets/Emails/") + Name);

            if (Name != "" && File.Exists(file_name))
            {
                string Text = "";
                StreamReader SR = File.OpenText(file_name);
                string S = SR.ReadLine();
                Text = S;
                while (S != null)
                {
                    S = SR.ReadLine();

                    Text = Text + S;
                }
                SR.Close();
                return Text;
            }
            else
            {
                return "";
            }
        }
        public static bool SendMail(string ToEmail, string EmailTemplateName, System.Collections.Specialized.NameValueCollection parameters, HttpFileCollection files = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ToEmail))
                {
                    Helpers.WriteLogToFile.Write("SendEmail function error. To Email is missing " + EmailTemplateName);
                    return false;
                }

                string body = Get_Template(EmailTemplateName);
                string subject = System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString();

                if (!string.IsNullOrEmpty(body))
                {
                    foreach (string s in parameters.AllKeys)
                        body = body.Replace("#%" + s + "%#", parameters[s]);//s.ToUpper()

                    if (EmailTemplateName.Equals("Email_Activation.htm"))
                    {
                        subject = "Welcome to " + System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString() + "";
                    }
                    if (EmailTemplateName.Equals("PasswordReset.htm"))
                    {
                        subject = "Having trouble accessing " + System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString() + "?";
                    }
                    //else
                    //{ Helpers.WriteLogToFile.Write("Subject Is missing for template: " + EmailTemplateName); }

                    return Send(string.Empty, string.Empty, ToEmail, ToEmail, subject, body, true, null, files);
                }
                else
                {
                    Helpers.WriteLogToFile.Write("template not found: " + EmailTemplateName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                return false;
            }
        }
    }
}