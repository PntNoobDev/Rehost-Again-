using System.Activities;
using System.Activities.Presentation;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Rehost_Again_
{
    [ToolboxBitmap(typeof(resfinder), "/Resources/pdf.png")]
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
