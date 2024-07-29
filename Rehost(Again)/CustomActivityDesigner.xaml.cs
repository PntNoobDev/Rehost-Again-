using System.Activities;
using System.Activities.Presentation;
using System.Windows;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Rehost_Again_
{
    public partial class CustomActivityDesigner : ActivityDesigner
    {
        public CustomActivityDesigner()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rar,Zip,Raz (*.Rar,*.Zip,*.Raz)|*.Rar;*.Zip;*.Raz"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                inputTextBlock.Text = openFileDialog.FileName;
                // Đặt giá trị cho ModelItem
                ModelItem.Properties["InputFilePath"].SetValue(new InArgument<string>(openFileDialog.FileName));
            }
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outputTextBlock.Text = folderBrowserDialog.SelectedPath;
                    // Đặt giá trị cho ModelItem
                    ModelItem.Properties["OutputFolderPath"].SetValue(new InArgument<string>(folderBrowserDialog.SelectedPath));
                }
            }
        }
    }
}
