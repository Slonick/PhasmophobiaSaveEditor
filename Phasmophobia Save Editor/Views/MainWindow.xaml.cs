using PhasmophobiaSaveEditor.ViewModels;

namespace PhasmophobiaSaveEditor.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}