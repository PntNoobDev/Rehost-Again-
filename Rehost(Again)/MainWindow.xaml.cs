using System;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.Services;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.Activities;
using System.Activities.Core.Presentation;
using System.ComponentModel;

namespace Rehost_Again_
{
    public partial class MainWindow : Window
    {
        private WorkflowDesigner wd;
        private WorkflowApplication wfApp;
        private bool isWorkflowRunning = false;
        private Action<string> appendTextAction;

        public MainWindow()
        {
            InitializeComponent();
            RegisterMetadata();
            AddDesigner();
            AddToolBox();
            AddPropertyInspector();
            RegisterMetadata1();

            // Initialize appendTextAction
            appendTextAction = text => Dispatcher.Invoke(() =>
            {
                txtOutput.AppendText(text);
                txtOutput.ScrollToEnd();
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Run a simple workflow when the application starts
            RunInitialWorkflow();
        }

        private void AddDesigner()
        {
            wd = new WorkflowDesigner();
            designerGrid.Children.Add(wd.View);

            // Load a simple Sequence activity with While and Delay to ensure we have a valid workflow
            wd.Load(new Sequence
            {
                Activities =
                {
                    new WriteLine { Text = "Starting Workflow..." },
                    new While
                    {
                        Condition = new VisualBasicValue<bool>("True"),
                        Body = new Sequence
                        {
                            Activities =
                            {
                                new WriteLine { Text = "Inside While loop" },
                                new Delay { Duration = new TimeSpan(0, 0, 5) } // Delay 5 seconds
                            }
                        }
                    }
                }
            });

            wd.ModelChanged += Wd_ModelChanged;
        }

        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();

            // Register custom activity and its designer
            AttributeTableBuilder builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(
                typeof(CustomActivity),
                new DesignerAttribute(typeof(CustomActivityDesigner)));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private void RegisterMetadata1()
        {
            var dm = new DesignerMetadata();
            dm.Register();

            // Register custom activity and its designer
            AttributeTableBuilder builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(
                typeof(ReadPdfActivity),
                new DesignerAttribute(typeof(ReadPdfActivityDesigner))
            );
            MetadataStore.AddAttributeTable(builder.CreateTable());
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
            var tool6 = new ToolboxItemWrapper("Rehost_Again_.CustomActivity",
                typeof(CustomActivity).Assembly.FullName, null, "CustomActivity");
            var tool7 = new ToolboxItemWrapper("Rehost_Again_.ReadPdfActivity",
                typeof(ReadPdfActivity).Assembly.FullName, null, "ReadPdfActivity");

            category.Add(tool1);
            category.Add(tool2);
            category.Add(tool3);
            category.Add(tool4);
            category.Add(tool5);
            category.Add(tool6);
            category.Add(tool7);

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
            if (isWorkflowRunning)
                return;

            if (wd != null)
            {
                var activity = wd.Context.Services.GetService<ModelService>().Root.GetCurrentValue() as Activity;
                if (activity != null)
                {
                    var textWriter = new WorkflowTextWriter(appendTextAction);

                    // Create WorkflowApplication
                    wfApp = new WorkflowApplication(activity);

                    // Add extension
                    wfApp.Extensions.Add(textWriter);

                    wfApp.Completed += WfApp_Completed;
                    wfApp.Aborted += WfApp_Aborted;

                    isWorkflowRunning = true;
                    UpdateUI();

                    wfApp.Run();
                }
            }
        }

        private void RunInitialWorkflow()
        {
            var initialWorkflow = new Sequence
            {
                Activities =
                {
                    new WriteLine { Text = "Application Started: Workflow is running..." },
                    new Delay { Duration = TimeSpan.FromSeconds(2) }, // Delay for 2 seconds
                    new WriteLine { Text = "Initial workflow completed." }
                }
            };

            var textWriter = new WorkflowTextWriter(appendTextAction);

            var initialWfApp = new WorkflowApplication(initialWorkflow);
            initialWfApp.Extensions.Add(textWriter);

            initialWfApp.Run();
        }

        private void WfApp_Completed(WorkflowApplicationCompletedEventArgs obj)
        {
            isWorkflowRunning = false;
            UpdateUI();
        }

        private void WfApp_Aborted(WorkflowApplicationAbortedEventArgs obj)
        {
            isWorkflowRunning = false;
            UpdateUI();
        }
    }

    public class WorkflowTextWriter : TextWriter
    {
        private readonly Action<string> appendTextAction;

        public WorkflowTextWriter(Action<string> appendTextAction)
        {
            this.appendTextAction = appendTextAction;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            appendTextAction(value.ToString());
        }

        public override void Write(string value)
        {
            appendTextAction(value);
        }
    }
}
