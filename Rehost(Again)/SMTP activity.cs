using System;
using System.Activities;
using System.Activities.Tracking;
using System.ComponentModel;
using System.Windows.Markup;

namespace Rehost_Again_
{
    [ContentProperty("Body")]
    public class SmtpActivity : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> To { get; set; }

        [RequiredArgument]
        public InArgument<string> Subject { get; set; }

        [RequiredArgument]
        public InArgument<string> Body { get; set; }

        public InArgument<string> SmtpServer { get; set; }
        public InArgument<int> SmtpPort { get; set; }
        public InArgument<string> Username { get; set; }
        public InArgument<string> Password { get; set; }
        public SmtpSecurityMode SecurityMode { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var to = To.Get(context);
            var subject = Subject.Get(context);
            var body = Body.Get(context);
            var smtpServer = SmtpServer.Get(context);
            var smtpPort = SmtpPort.Get(context);
            var username = Username.Get(context);
            var password = Password.Get(context);
            var securityMode = SecurityMode;

            try
            {
                var smtpClient = new System.Net.Mail.SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new System.Net.NetworkCredential(username, password),
                    EnableSsl = securityMode == SmtpSecurityMode.SslOnConnect || securityMode == SmtpSecurityMode.StartTls
                };

                if (securityMode == SmtpSecurityMode.StartTls)
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.TargetName = smtpServer;
                }

                var mailMessage = new System.Net.Mail.MailMessage
                {
                    From = new System.Net.Mail.MailAddress(username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                smtpClient.Send(mailMessage);

                context.Track(new CustomTrackingRecord("EmailSent")
                {
                    Data = { ["Status"] = "Success", ["Message"] = "Email sent successfully." }
                });
            }
            catch (Exception ex)
            {
                context.Track(new CustomTrackingRecord("EmailSent")
                {
                    Data = { ["Status"] = "Failure", ["Message"] = ex.Message }
                });
                throw;
            }
        }
    }
}
