using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities;
using System.Activities.Presentation;
using System.Collections.ObjectModel;

namespace Rehost_Again_
{
    /// <summary>
    /// Interaction logic for HttpRequestDesigner.xaml
    /// </summary>
    public partial class HttpRequestDesigner : ActivityDesigner
    {
        public HttpRequestDesigner()
        {
            InitializeComponent();
            this.DataContext = new HttpRequestViewModel();
            this.DataContext = this.ModelItem;

        }
        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            InitialView.Visibility = Visibility.Collapsed;
            DetailedView.Visibility = Visibility.Visible;
        }
        private void AddParameter_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật TextBlocks và ComboBox để hiển thị "GetOrPost"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "GetOrPost" };
            TypeComboBox.SelectedIndex = 0;
        }

        private void AddHeader_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật TextBlocks và ComboBox để hiển thị "Http Header"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "Http Header" };
            TypeComboBox.SelectedIndex = 0;
        }

        private void AddUrl_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật TextBlocks và ComboBox để hiển thị "UrlSegment"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "UrlSegment" };
            TypeComboBox.SelectedIndex = 0;
        }


        private void AddAttachment_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteAttachment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve data from UI elements
            var endPoint = EndPointTextBox.Expression.ToString();
            var previewUrl = PreviewUrlTextBox.Expression.ToString();
            var timeout = TimeoutTextBox.Expression.ToString();
            var clientCertificate = ClientCertificateTextBox.Expression.ToString();
            var clientCertificatePassword = ClientCertificateTextBox1.Expression.ToString();
            var requestMethod = ((ComboBoxItem)RequestMethodComboBox.SelectedItem)?.Content.ToString() ?? "Not Selected";
            var acceptResponseAs = ((ComboBoxItem)AccpetResponseAsCombobox.SelectedItem)?.Content.ToString() ?? "Not Selected";

            // Display a preview of the request (this is a placeholder; adjust as needed)
            MessageBox.Show($"Preview:\nEndpoint: {endPoint}\nPreview URL: {previewUrl}\nTimeout: {timeout}\n" +
                            $"Client Certificate: {clientCertificate}\nClient Certificate Password: {clientCertificatePassword}\n" +
                            $"Request Method: {requestMethod}\nAccept Response As: {acceptResponseAs}",
                            "Request Preview");
        }


        // Event handler for "OK" button click
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Validate and save the data from the UI
            var viewModel = (HttpRequestViewModel)DataContext;
            if (viewModel != null)
            {
                // Update the ViewModel properties
                viewModel.Endpoint = EndPointTextBox.Expression.ToString();
                viewModel.Preview = PreviewUrlTextBox.Expression.ToString();
                viewModel.TimeOut = TimeoutTextBox.Expression.ToString();
                viewModel.ClientCertificate = ClientCertificateTextBox.Expression.ToString();
                viewModel.ClientCertificatePassword = ClientCertificateTextBox1.Expression.ToString();
                viewModel.RequestMethod = ((ComboBoxItem)RequestMethodComboBox.SelectedItem).Content.ToString();
                viewModel.AcceptResponseAs = ((ComboBoxItem)AccpetResponseAsCombobox.SelectedItem).Content.ToString();

                // Perform any additional actions needed for saving or processing the data

                // Close or hide the designer (if needed)
                this.Visibility = Visibility.Collapsed; // Or handle closing in another way
            }
        }
        public ObservableCollection<RequestMethod> RequestMethods
        {
            get
            {
                return new ObservableCollection<RequestMethod>(Enum.GetValues(typeof(RequestMethod)).Cast<RequestMethod>());
            }
        }

        public ObservableCollection<AcceptResponseAsMode> AcceptResponseAsMode
        {
            get
            {
                return new ObservableCollection<AcceptResponseAsMode>(Enum.GetValues(typeof(AcceptResponseAsMode)).Cast<AcceptResponseAsMode>());
            }
        }
        /* public enum RequestMethod
         {
             GET,
             POST,
             PUT,
             DELETE
         }*/


    }
}

