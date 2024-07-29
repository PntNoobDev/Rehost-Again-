using System;
using System.Activities;

namespace Rehost_Again_
{
    public class CustomActivity : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> InputFilePath { get; set; }

        [RequiredArgument]
        public InArgument<string> OutputFolderPath { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputFilePath = InputFilePath.Get(context);
            var outputFolderPath = OutputFolderPath.Get(context);

            // Implement your custom activity logic here
        }
    }
}
