using System.Threading.Tasks;

namespace PhasmophobiaSaveEditor.Configuration
{
    public interface IConfigurationManager<out T>
    {
        T Config { get; }

        string DataFolder { get; }

        void Load();
        Task LoadAsync();
        void Save();
        Task SaveAsync();
    }
}