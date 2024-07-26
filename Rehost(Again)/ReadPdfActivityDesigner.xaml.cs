using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Activities.Presentation;

namespace Rehost_Again_
{
    public partial class ReadPdfActivityDesigner : ActivityDesigner
    {
        public ReadPdfActivityDesigner()
        {
            InitializeComponent();
        }

        private void SelectPdfFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Select a PDF File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                PdfFilePathTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
