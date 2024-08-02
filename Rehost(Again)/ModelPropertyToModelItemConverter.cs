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
            var property = value as ModelProperty;
            return property?.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
