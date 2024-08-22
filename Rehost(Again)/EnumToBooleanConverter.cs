    using System.Globalization;
    using System.Windows.Data;
    using System.Activities.Presentation.Model;
    using System.Activities.Expressions;
    using System.Activities;
    using System;

    namespace Rehost_Again_
    {
    
        public class EnumToBooleanConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is InArgument<string>)
                {
                    Activity<string> expression = ((InArgument<string>)value).Expression;
                    if (expression is Literal<string>)
                    {
                        return ((Literal<string>)expression).Value;
                    }
                }
                var test = value as System.Activities.Presentation.Model.ModelItem;
                if (test != null)
                {
                    if (test.ItemType == typeof(InArgument<string>))
                    {
                        var val = test.Properties["Expression"].Value;
                        if (val != null)
                        {
                            var valstring = val.ToString();
                            return valstring;
                        }
                        return val;
                    }
                }
                return null;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is string)
                {
                    return new InArgument<string>(new Literal<string>((string)value));
                }
                else
                {
                    return null;
                }
            }

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

            private void OnPropertyChanged(string v)
            {
                throw new NotImplementedException();
            }
        }
    }