using System.Runtime.Serialization;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.Models
{
    [DataContract]
    public class BaseData<T> : BaseBindable
    {
        private T value;

        [DataMember(Name = "Key")]
        public string Key { get; set; }

        [DataMember(Name = "Value")]
        public T Value
        {
            get => this.value;
            set => this.Set(ref this.value, value);
        }

        public override string ToString() => $"{this.Key}: {this.Value}";
    }
}