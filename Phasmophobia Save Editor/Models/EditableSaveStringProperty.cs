namespace PhasmophobiaSaveEditor.Models
{
    internal class EditableSaveStringProperty : EditableSaveProperty<string>
    {
        public EditableSaveStringProperty(BaseData<string> data, bool isReadOnly) : base(data)
        {
            this.IsReadOnly = isReadOnly;
        }

        public bool IsReadOnly { get; set; }
    }
}