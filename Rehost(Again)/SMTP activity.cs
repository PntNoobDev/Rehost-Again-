using System.Activities;
using System.ComponentModel;

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
            // Implement SMTP send logic here
        }
    }
}
