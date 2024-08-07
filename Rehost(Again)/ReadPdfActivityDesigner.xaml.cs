using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                // Đặt giá trị cho ModelItem
                ModelItem.Properties["PdfFilePath"].SetValue(new InArgument<string>(openFileDialog.FileName));

                // Hiển thị đường dẫn file PDF trong TextBox
                PdfFilePathTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
