using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System;

namespace Rehost
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            
            SelectedProperties = new ControlProperties();
            DataContext = this; 

            
            RegisterMetadata();

          
            AddDesigner();
        }

        private WorkflowDesigner wd;


        private void AddDesigner()
        {
            // Create an instance of WorkflowDesigner class.
            this.wd = new WorkflowDesigner();

            // Place the designer canvas in the middle column of the grid.
            Grid.SetColumn(this.wd.View, 1);

            // Load a new Sequence as default.
            this.wd.Load(new Sequence());
            abcontrol.
            Content1.Children.Add(this.wd.View);
        }

        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();
        }


        public ControlProperties SelectedProperties { get; set; }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem)
            {
                if (selectedItem.Header is StackPanel panel)
                {
                    var textBlock = panel.Children.OfType<TextBlock>().FirstOrDefault();
                    if (textBlock != null)
                    {
                        switch (textBlock.Text)
                        {
                            case "Button":
                                SelectedProperties.ControlName = "Button";
                                SelectedProperties.Width = 100;
                                SelectedProperties.Height = 50;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-button-50.png";
                                break;
                            case "Label":
                                SelectedProperties.ControlName = "Label";
                                SelectedProperties.Width = 100;
                                SelectedProperties.Height = 50;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-label-30.png";
                                break;
                            case "TextBox":
                                SelectedProperties.ControlName = "TextBox";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-text-input-form-32.png";
                                break;
                            case "Radio Button":
                                SelectedProperties.ControlName = "Radio Button";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-radio-button-24.png";
                                break;
                            case "GridView":
                                SelectedProperties.ControlName = "GridView";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-grid-view-24.png";
                                break;
                            case "Image":
                                SelectedProperties.ControlName = "Image";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa//icons8-image-48.png";
                                break;
                            case "ListView":
                                SelectedProperties.ControlName = "ListView";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-view-48.png";
                                break;
                            case "CheckBox":
                                SelectedProperties.ControlName = "CheckBox";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/checkbox.png";
                                break;
                            case "DateTime":
                                SelectedProperties.ControlName = "DateTime";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-schedule-50.png";
                                break;
                            case "TabControl":
                                SelectedProperties.ControlName = "TabControl";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-tab-key-48.png";
                                break;
                            case "Pointer":
                                SelectedProperties.ControlName = "Pointer";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-pointer-30.png";
                                break;
                            case "Combobox":
                                SelectedProperties.ControlName = "Combobox";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/combobox.png";
                                break;
                            case "ToolBar":
                                SelectedProperties.ControlName = "ToolBar";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-toolbar-32.png";
                                break;
                            case "DockPanel":
                                SelectedProperties.ControlName = "DockPanel";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/dockpanel.png";
                                break;
                            case "TreeView":
                                SelectedProperties.ControlName = "TreeView";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/treeview.png";
                                break;
                            case "Srcoll Bar":
                                SelectedProperties.ControlName = "Srcoll Bar";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-scroll-bar-48.png";
                                break;
                            case "StackPanel":
                                SelectedProperties.ControlName = "Stack Panel";
                                SelectedProperties.Width = 200;
                                SelectedProperties.Height = 30;
                                SelectedProperties.ImagePath = "file:///E:/images-chuongtrinhquanlykytucxa/icons8-grid-view-24.png";
                                break;

                                // Add more cases for other controls as needed
                        }
                    }
                }
            }
        }

        private void RibbonTab_Loaded(object sender, RoutedEventArgs e)
        {
            RibbonTab selectedTab = (RibbonTab)sender;
            MessageBox.Show($"Loaded Tab: {selectedTab.Header}");
        }

        private void AddProperty(string propertyName, string propertyValue)
        {
            StackPanel propertyStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            TextBlock nameLabel = new TextBlock
            {
                Text = propertyName + ": "
            };
            propertyStackPanel.Children.Add(nameLabel);

            TextBox valueTextBox = new TextBox
            {
                Text = propertyValue
            };
            propertyStackPanel.Children.Add(valueTextBox);
        }

        private void Select(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Your code here
        }

        private void ShowNotification(string message)
        {
            // Clear old notifications
            NotificationGrid.Children.Clear();

            // Create a TextBlock to display the notification
            TextBlock textBlock = new TextBlock
            {
                Text = message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Add TextBlock to Grid
            NotificationGrid.Children.Add(textBlock);
        }

        private void ToolBox_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem)
            {
                // Lấy thông tin từ TreeViewItem được chọn
                var stackPanel = selectedItem.Header as StackPanel;
                if (stackPanel != null)
                {
                    var textBlock = stackPanel.Children[1] as TextBlock;
                    if (textBlock != null)
                    {
                        string controlName = textBlock.Text;

                        // Giả sử bạn có một lớp ControlProperties để lưu các thuộc tính
                        var properties = new ControlProperties
                        {
                            ControlName = controlName,
                            // Các thuộc tính khác nếu cần
                        };

                        // Liên kết dữ liệu với phần PropertiesPanel
                     
                    }
                }
            }

        }
        private Grid NotificationGrid;


        public class ControlProperties
        {
            public string ControlName { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public string ImagePath { get; set; }
        }
    }
}
