using System;
using System.Globalization;
using System.Windows.Data;
using System.Activities.Presentation.Model;

namespace Rehost_Again_
{
    public class ModelPropertyToModelItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modelProperty = value as ModelProperty;
            return modelProperty?.ComputedValue;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
