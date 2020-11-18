namespace PhasmophobiaSaveEditor.Models
{
    internal class EditableSaveProperty<T>
    {
        public EditableSaveProperty() { }

        public EditableSaveProperty(BaseData<T> data)
        {
            this.Data = data;
        }

        public BaseData<T> Data { get; set; }
    }
}