using System.Activities.Presentation;
using System.Windows.Controls;

namespace Rehost_Again_
{
    public partial class SmtpActivityDesigner : ActivityDesigner
    {
        public SmtpActivityDesigner()
        {
            InitializeComponent();
            this.DataContext = this.ModelItem;
        }

    }
}
