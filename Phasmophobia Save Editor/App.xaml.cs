using System.ComponentModel;
using System.Windows;
using PhasmophobiaSaveEditor.Configuration;
using PhasmophobiaSaveEditor.Controls.Dialog;
using PhasmophobiaSaveEditor.Handlers;
using PhasmophobiaSaveEditor.Logging;
using PhasmophobiaSaveEditor.Models.Configuration;
using PhasmophobiaSaveEditor.Utils;
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

            //Logger
            var loggerConfig = LoggerConfigBuilder.Create(true, LogLevel.Trace)
                                                  .WithLayout("[${assembly-name} ${assembly-version}] [${longdate}] [${level}] ${logger:short}: ${message}")
                                                  .UseDebugLogger()
                                                  .UseFileLogger(PathUtil.LogFile);

            LogManager.Default.SetConfig(loggerConfig);

            ExceptionHandler.Current.RegisterHandler();

            var config = new ConfigurationManager<UserConfigOptions>();
            config.Load();
            config.Config.AppearanceOptions.RestoreTo(Current.MainWindow);

            Current.MainWindow.Closing += this.OnMainWindowClosing;

            if (!config.Config.GeneralAgreementsIsAccepted)
            {
                var isAccepted = MessageDialog.ShowDialog(new DialogParameters
                {
                    Button = DialogButton.YesNo,
                    Content = LocalizationManager.GetLocalizationString("Main.GeneralAgreementsText"),
                    DialogStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = DialogIcon.Question,
                    Header = LocalizationManager.GetLocalizationString("Main.GeneralAgreements"),
                });

                if (isAccepted != true)
                {
                    Current.Shutdown();
                    return;
                }

                config.Config.GeneralAgreementsIsAccepted = true;
                config.Save();
            }

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