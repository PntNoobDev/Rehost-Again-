using System;
using System.Globalization;
using System.Windows.Data;
using System.Activities;
using System.Activities.Expressions;

namespace Rehost_Again_
{
    public class EnumToBooleanConverter : IValueConverter
    {
        // Chuyển đổi từ giá trị vào thành đối tượng boolean
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

        // Chuyển đổi từ giá trị boolean về đối tượng InArgument<string>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return new InArgument<string>(new Literal<string>(strValue));
            }

            // Nếu giá trị không phải string, trả về null
            return null;
        }

        public object Convert1(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
        public object ConvertBack1(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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

        // Không cần thiết nếu không sử dụng INotifyPropertyChanged
        // Nếu cần thiết, triển khai sự kiện PropertyChanged
        private string _acceptResponseAs;
        public string AcceptResponseAs
        {
            get => _acceptResponseAs;
            set
            {
                if (_acceptResponseAs != value)
                {
                    _acceptResponseAs = value;
                    OnPropertyChanged(nameof(AcceptResponseAs));
                }
            }
        }

        // Triển khai hoặc loại bỏ nếu không sử dụng
        private void OnPropertyChanged(string propertyName)
        {
            // Nếu cần thông báo thay đổi thuộc tính
            // Implement PropertyChanged event logic here
        }
    }
}
