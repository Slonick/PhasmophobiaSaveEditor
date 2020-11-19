using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.ViewModels
{
    public class SetAllItemsViewModel : BaseBindable
    {
        public int MaxValue { get; set; } = 999;
        public int MinValue { get; set; } = 0;
        public int Step { get; set; } = 5;
        public int Value { get; set; }
    }
}