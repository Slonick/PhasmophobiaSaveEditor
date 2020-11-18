namespace PhasmophobiaSaveEditor.Models
{
    internal class EditableSaveIntProperty : EditableSaveProperty<int>
    {
        public EditableSaveIntProperty(BaseData<int> data, int minimum, int maximum, int step) : base(data)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Step = step;
        }

        public int Maximum { get; set; }
        public int Minimum { get; set; }
        public int Step { get; set; }
    }
}