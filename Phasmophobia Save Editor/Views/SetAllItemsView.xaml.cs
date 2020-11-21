using PhasmophobiaSaveEditor.ViewModels;

namespace PhasmophobiaSaveEditor.Views
{
    public partial class SetAllItemsView
    {
        public SetAllItemsView()
        {
            this.InitializeComponent();
        }

        public SetAllItemsView(SetAllItemsViewModel vm) : this()
        {
            this.DataContext = vm;
        }
    }
}