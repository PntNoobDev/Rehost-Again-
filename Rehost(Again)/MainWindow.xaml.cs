using System;
using System.Windows;
using System.Windows.Controls;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.Activities.Core.Presentation;
using System.Activities.Hosting;
using System.Activities.Presentation.Services;

namespace Rehost_Again_
{
    public partial class MainWindow : Window
    {
        private WorkflowDesigner wd;
        private WorkflowApplication wfApp;
        private bool isWorkflowRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            RegisterMetadata();
            AddDesigner();
            AddToolBox();
            AddPropertyInspector();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Handle the Loaded event here if needed
        }

        private void AddDesigner()
        {
            wd = new WorkflowDesigner();
            designerGrid.Children.Add(wd.View);

            // Load a simple Sequence activity to ensure we have a valid workflow
            wd.Load(new Sequence
            {
                Activities =
                {
                    new WriteLine { Text = "Hello, Workflow!" }
                }
            });

            wd.ModelChanged += Wd_ModelChanged;
        }

        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();
        }

        private ToolboxControl GetToolboxControl()
        {
            var ctrl = new ToolboxControl();
            var category = new ToolboxCategory("category1");

            var tool1 = new ToolboxItemWrapper("System.Activities.Statements.Assign",
                typeof(Assign).Assembly.FullName, null, "Assign");
            var tool2 = new ToolboxItemWrapper("System.Activities.Statements.Sequence",
                typeof(Sequence).Assembly.FullName, null, "Sequence");
            var tool3 = new ToolboxItemWrapper("System.Activities.Statements.WriteLine",
                typeof(WriteLine).Assembly.FullName, null, "WriteLine");
            var tool4 = new ToolboxItemWrapper("System.Activities.Statements.While",
                typeof(While).Assembly.FullName, null, "While");
            var tool5 = new ToolboxItemWrapper("System.Activities.Statements.Delay",
                typeof(Delay).Assembly.FullName, null, "Delay");

            category.Add(tool1);
            category.Add(tool2);
            category.Add(tool3);
            category.Add(tool4);
            category.Add(tool5);

            ctrl.Categories.Add(category);
            return ctrl;
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolboxControl();
            Grid.SetColumn(tc, 0);
            toolboxGrid.Children.Add(tc);
        }

        private void AddPropertyInspector()
        {
            Grid.SetColumn(wd.PropertyInspectorView, 2);
            propertyGrid.Children.Add(wd.PropertyInspectorView);
        }

        private void Wd_ModelChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            Dispatcher.Invoke(() =>
            {
                if (wd != null && !isWorkflowRunning)
                {
                    btnPlay.IsEnabled = true;
                    btnStop.IsEnabled = false;
                }
                else if (isWorkflowRunning)
                {
                    btnPlay.IsEnabled = false;
                    btnStop.IsEnabled = true;
                }
                else
                {
                    btnPlay.IsEnabled = false;
                    btnStop.IsEnabled = false;
                }
            });
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            RunWorkflow();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (wfApp != null)
            {
                wfApp.Cancel();

                wfApp.Completed -= WfApp_Completed;
                wfApp.Aborted -= WfApp_Aborted;
                wfApp = null;

                isWorkflowRunning = false;
                UpdateUI();
            }
        }

        private void RunWorkflow()
        {
            if (wd != null && wfApp == null)
            {
                var activity = wd.Context.Services.GetService<ModelService>().Root.GetCurrentValue() as Activity;

                if (activity != null)
                {
                    wfApp = new WorkflowApplication(activity);

                    wfApp.Completed += WfApp_Completed;
                    wfApp.Aborted += WfApp_Aborted;
                    wfApp.OnUnhandledException += WfApp_OnUnhandledException;

                    wfApp.Extensions.Add(new TextWriterExtension(txtOutput));

                    wfApp.Run();

                    isWorkflowRunning = true;
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("The workflow definition is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void WfApp_Completed(WorkflowApplicationCompletedEventArgs obj)
        {
            Dispatcher.Invoke(() =>
            {
                isWorkflowRunning = false;
                UpdateUI();
            });
        }

        private void WfApp_Aborted(WorkflowApplicationAbortedEventArgs obj)
        {
            Dispatcher.Invoke(() =>
            {
                isWorkflowRunning = false;
                UpdateUI();
            });
        }

        private UnhandledExceptionAction WfApp_OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            MessageBox.Show($"Unhandled exception: {arg.UnhandledException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return UnhandledExceptionAction.Terminate;
        }

        private class TextWriterExtension : System.IO.TextWriter
        {
            private readonly TextBox output;

            public TextWriterExtension(TextBox output)
            {
                this.output = output;
            }

            public override void Write(char value)
            {
                output.Dispatcher.Invoke(() =>
                {
                    output.AppendText(value.ToString());
                    output.ScrollToEnd();
                });
            }

            public override void WriteLine(string value)
            {
                output.Dispatcher.Invoke(() =>
                {
                    output.AppendText(value + Environment.NewLine);
                    output.ScrollToEnd();
                });
            }

            public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
        }
    }
}
