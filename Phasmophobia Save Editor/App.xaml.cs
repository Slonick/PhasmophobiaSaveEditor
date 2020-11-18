using System.ComponentModel;
using System.Windows;
using PhasmophobiaSaveEditor.Configuration;
using PhasmophobiaSaveEditor.Handlers;
using PhasmophobiaSaveEditor.Models.Configuration;
using PhasmophobiaSaveEditor.ViewModels;
using PhasmophobiaSaveEditor.Views;

namespace PhasmophobiaSaveEditor
{
    public partial class App : Application
    {
        public App()
        {
            OneInstanceHandler.Current.RegisterHandler();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.MainWindow = new MainWindow();
            ExceptionHandler.Current.RegisterHandler();

            var config = new ConfigurationManager<UserConfigOptions>();
            config.Load();
            config.Config.AppearanceOptions.RestoreTo(Current.MainWindow);

            Current.MainWindow.Closing += this.OnMainWindowClosing;
            Current.MainWindow.Show();

            if (Current.MainWindow.DataContext is MainViewModel vm)
            {
                vm.Init();
            }
        }

        private void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            var config = new ConfigurationManager<UserConfigOptions>();
            config.Load();
            config.Config.AppearanceOptions.UpdateFrom(Current.MainWindow);

            config.Save();
        }
    }
}