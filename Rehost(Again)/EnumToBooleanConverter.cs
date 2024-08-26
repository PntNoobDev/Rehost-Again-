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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is InArgument<string> inArg)
            {
                Activity<string> expression = inArg.Expression;
                if (expression is Literal<string> literal)
                {
                    return literal.Value;
                }
            }

            // Nếu giá trị không phải InArgument<string>, trả về giá trị null
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

        public object Convert1(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is InArgument<bool> inArg)
            {
                Activity<bool> expression = inArg.Expression;
                if (expression is Literal<bool> literal)
                {
                    return literal.Value.ToString();
                }
            }
            return null;
        }

        // Chuyển đổi từ chuỗi về đối tượng InArgument<bool>
        public object ConvertBack1(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                if (bool.TryParse(strValue, out bool boolValue))
                {
                    return new InArgument<bool>(new Literal<bool>(boolValue));
                }
            }
            return null;
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
