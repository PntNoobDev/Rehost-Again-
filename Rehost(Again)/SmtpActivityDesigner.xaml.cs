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
            // To Field
            BindProperty("ToTextBox", "To");

            // Subject Field
            BindProperty("SubjectTextBox", "Subject");

            // Body Field
            BindProperty("BodyTextBox", "Body");
        }

        private void BindProperty(string textBoxName, string propertyName)
        {
            var textBox = FindName(textBoxName) as TextBox;
            if (textBox != null)
            {
                var modelProperty = this.ModelItem.Properties[propertyName];
                textBox.Text = modelProperty?.ComputedValue as string ?? string.Empty;

                // Update the property when the TextBox value changes
                textBox.TextChanged += (s, e) =>
                {
                    modelProperty?.SetValue(new InArgument<string>(textBox.Text));
                };
            }
        }
    }
}

