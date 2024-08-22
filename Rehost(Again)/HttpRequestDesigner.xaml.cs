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
        }
        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            InitialView.Visibility = Visibility.Collapsed;
            DetailedView.Visibility = Visibility.Visible;
        }
        private void AddParameter_Click(object sender, RoutedEventArgs e)
        {
           /* // Cập nhật TextBlocks và ComboBox để hiển thị "GetOrPost"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "GetOrPost" };
            TypeComboBox.SelectedIndex = 0;*/
        }

        private void AddHeader_Click(object sender, RoutedEventArgs e)
        {
          /*  // Cập nhật TextBlocks và ComboBox để hiển thị "Http Header"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "Http Header" };
            TypeComboBox.SelectedIndex = 0;*/
        }

        private void AddUrl_Click(object sender, RoutedEventArgs e)
        {
          /*  // Cập nhật TextBlocks và ComboBox để hiển thị "UrlSegment"
            NameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            TypeComboBox.ItemsSource = new List<string> { "UrlSegment" };
            TypeComboBox.SelectedIndex = 0;*/
        }
        
        private void AddAttachment_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteAttachment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
