using System.Runtime.Serialization;
using PhasmophobiaSaveEditor.Configuration.Attributes;

namespace PhasmophobiaSaveEditor.Models.Configuration
{
    [Configuration(Filename = "config.json")]
    [DataContract]
    public class UserConfigOptions
    {
        public UserConfigOptions()
        {
            this.AppearanceOptions = new AppearanceOptions();
        }

        [DataMember]
        public AppearanceOptions AppearanceOptions { get; set; }

        [DataMember]
        public bool GeneralAgreementsIsAccepted { get; set; }
    }
}