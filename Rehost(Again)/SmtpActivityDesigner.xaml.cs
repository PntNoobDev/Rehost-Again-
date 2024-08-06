using System.Activities.Expressions;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace Rehost_Again_
{
    public partial class SmtpActivityDesigner
    {
        public SmtpActivityDesigner()
        {
            InitializeComponent();
            this.Loaded += SmtpActivityDesigner_Loaded;
            this.DataContext = this;
        }

        private void SmtpActivityDesigner_Loaded(object sender, RoutedEventArgs e)
        {
            BindProperties();

        }

        private void BindProperties()
        {
            // Binding for To property
            var toProperty = this.ModelItem.Properties["To"];
            UpdateTextBox("ToTextBox", toProperty);

            // Binding for Subject property
            var subjectProperty = this.ModelItem.Properties["Subject"];
            UpdateTextBox("SubjectTextBox", subjectProperty);

            // Binding for Body property
            var bodyProperty = this.ModelItem.Properties["Body"];
            UpdateTextBox("BodyTextBox", bodyProperty);
        }

        /*private void BindProperty(ModelProperty modelProperty, string textBoxName)
        {
            var binding = new Binding
            {
                Path = new PropertyPath("Value"),
                Mode = BindingMode.TwoWay,
                Source = modelProperty
            };

            var textBox = FindName(textBoxName) as TextBox;
            if (textBox != null)
            {
                textBox.SetBinding(TextBox.TextProperty, binding);
            }
        }*/
        private void UpdateTextBox(string textBoxName, ModelProperty modelProperty)
        {
            var textBox = FindName(textBoxName) as TextBox;
            if (textBox != null)
            {
                // Get the current value of the property
                var propertyValue = modelProperty?.ComputedValue as InArgument<string>;
                textBox.Text = propertyValue?.ToString() ?? string.Empty;

                // Handle changes in the TextBox and update the property
                textBox.TextChanged += (s, e) =>
                {
                    modelProperty?.SetValue(new InArgument<string>(textBox.Text));
                };
            }
        }
    }
}
