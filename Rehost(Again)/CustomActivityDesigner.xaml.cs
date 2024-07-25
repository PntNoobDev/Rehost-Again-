using Microsoft.Win32;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

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
                Filter = "All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputTextBlock.Text = openFileDialog.FileName;
            }
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outputTextBlock.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}


