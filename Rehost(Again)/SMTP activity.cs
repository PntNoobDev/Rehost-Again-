using System.Activities;
using System.ComponentModel;
using System.Activities.Presentation;
namespace Rehost_Again_
{
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
        }
    }
}
