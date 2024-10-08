﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Activities;
using System.Activities.Expressions;

namespace Rehost_Again_
{
    public class InArgumentBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is InArgument<bool>)
            {
                Activity<bool> expression = ((InArgument<bool>)value).Expression;
                if (expression is Literal<bool>)
                {
                    return ((Literal<bool>)expression).Value;
                }
            }
            var test = value as System.Activities.Presentation.Model.ModelItem;
            if (test != null)
            {
                if (test.ItemType == typeof(InArgument<bool>))
                {
                    var val = test.Properties["Expression"].Value;
                    if (val != null)
                    {
                        var valstring = val.ToString();
                        return bool.Parse(valstring);
                    }
                    return val;
                }
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return new InArgument<bool>(new Literal<bool>((bool)value));
            }
            else
            {
                return null;
            }
        }
    }
}
