using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhasmophobiaSaveEditor.Models
{
    [DataContract]
    public class PhasmophobiaSave
    {
        [DataMember(Name = "StringData")]
        public IList<BaseData<string>> StringData { get; set; }

        [DataMember(Name = "IntData")]
        public IList<BaseData<int>> IntData { get; set; }

        [DataMember(Name = "FloatData")]
        public IList<BaseData<float>> FloatData { get; set; }

        [DataMember(Name = "BoolData")]
        public IList<BaseData<bool>> BoolData { get; set; }
    }
}