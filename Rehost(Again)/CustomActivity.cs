using System;
using System.Activities;

namespace Rehost_Again_
{
    public class CustomActivity : CodeActivity
    {
        public InArgument<string> Input { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string input = context.GetValue(Input);
            Console.WriteLine(input);
        }
    }
}
