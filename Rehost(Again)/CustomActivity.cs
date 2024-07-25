using System;
using System.Activities;

namespace Rehost_Again_
{
    public class CustomActivity : NativeActivity
    {
        // Define an activity that can contain other activities
        public Activity Body { get; set; }
        public InArgument<string> Input { get; set; }

        // Override the Execute method to define the activity's behavior
        protected override void Execute(NativeActivityContext context)
        {
            // Retrieve the input argument
            string input = context.GetValue(Input);
            Console.WriteLine(input);

            // Schedule the body activity
            if (Body != null)
            {
                context.ScheduleActivity(Body);
            }
        }
    }
}
