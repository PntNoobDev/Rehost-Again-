using System;
using System.Activities;
using System.Collections.Generic;
using System.Drawing;

namespace Rehost_Again_
{
    [ToolboxBitmap(typeof(resfinder), "Resources.foreach.png")]



    public sealed class ForeachActivity<T> : NativeActivity
    {
        [RequiredArgument]
        public InArgument<IEnumerable<T>> Values { get; set; }

        public ActivityAction<T> Body { get; set; }
        public InArgument<T> Item { get; set; }
        public InArgument<string> ConditionDescription { get; set; }
        public string Type { get; set; } // To store the selected type as a string or another suitable type
        public Type ItemType { get; set; } = typeof(T);
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            // Cache metadata for the 'Values' argument
            var valuesArgument = new RuntimeArgument("Values", typeof(IEnumerable<T>), ArgumentDirection.In);
            metadata.Bind(Values, valuesArgument);
            metadata.AddArgument(valuesArgument);

            // Cache metadata for the 'Body' action
            if (Body != null)
            {
                metadata.AddDelegate(Body);
            }
            else
            {
                metadata.AddValidationError("Body must be set.");
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            IEnumerable<T> values = Values.Get(context);

            if (values == null)
            {
                return;
            }

            foreach (T item in values)
            {
                if (Body != null)
                {
                    context.ScheduleAction(Body, item);
                }
            }
        }
    }
}
