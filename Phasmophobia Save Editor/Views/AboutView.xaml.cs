using PhasmophobiaSaveEditor.ViewModels;

namespace PhasmophobiaSaveEditor.Views
{
    public partial class AboutView
    {
        public AboutView()
        {
            this.InitializeComponent();
            this.DataContext = new AboutViewModel();
        }
    }
}