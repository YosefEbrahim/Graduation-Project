using System.Net.Mail;

namespace File_Sharing_App.Helper.Mail
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _config;

        public MailHelper(IConfiguration config)
        {
            this._config = config;
        }
        public void SendMail(InputMailMessage model)
        {
            using (SmtpClient client = new SmtpClient(_config.GetValue<string>("Mail:Host"), _config.GetValue<int>("Mail:Port")))
            {
                var msg = new MailMessage();
                msg.To.Add(model.Email);
                msg.Subject = model.Subject;
                msg.Body = model.Body;
                msg.IsBodyHtml = true;
                msg.From = new MailAddress(_config.GetValue<string>("Mail:From"), _config.GetValue<string>("Mail:Sender"), System.Text.Encoding.UTF8);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("Mail:From"), _config.GetValue<string>("Mail:PWD"));
                client.Send(msg);


            }
        }

        public void SendMailToAdmin(InputMailMessage model)
        {
            using (SmtpClient client = new SmtpClient(_config.GetValue<string>("Mail:Host"), _config.GetValue<int>("Mail:Port")))
            {
                var msg = new MailMessage();
                msg.To.Add(_config.GetValue<string>("Mail:From"));
                msg.Subject = model.Subject;
                msg.Body = model.Body;
                msg.IsBodyHtml = true;
                msg.From = new MailAddress(model.Email, _config.GetValue<string>("Mail:Sender"), System.Text.Encoding.UTF8);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("Mail:From"), _config.GetValue<string>("Mail:PWD"));
                client.Send(msg);
            }
        }
    }
}
