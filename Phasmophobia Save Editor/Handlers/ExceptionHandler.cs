using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using PhasmophobiaSaveEditor.Controls;
using PhasmophobiaSaveEditor.Logging;
using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.Handlers
{
    public class ExceptionHandler
    {
        private static ExceptionHandler current;

        private readonly ILogger logger = LogManager.Default.GetCurrentClassLogger();
        private Exception prevException;

        public static ExceptionHandler Current => current ?? (current = new ExceptionHandler());

        public void RegisterHandler()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) => this.CurrentDomainOnUnhandledException(args);

            if (Application.Current.Dispatcher != null)
            {
                Application.Current.Dispatcher.UnhandledException +=
                    (sender, args) => this.DispatcherOnUnhandledException(args);
            }

            Application.Current.DispatcherUnhandledException +=
                (sender, args) => this.CurrentOnDispatcherUnhandledException(args);

            TaskScheduler.UnobservedTaskException +=
                (sender, args) => this.TaskSchedulerOnUnobservedTaskException(args);
        }

        private void CurrentDomainOnUnhandledException(UnhandledExceptionEventArgs args)
        {
            var exception = args.ExceptionObject as Exception;
            if (args.IsTerminating)
            {
                return;
            }

            this.logger.Fatal(exception);
            this.ShowMessage(exception);
        }

        private void CurrentOnDispatcherUnhandledException(DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            this.logger.Fatal(args.Exception);
            this.ShowMessage(args.Exception);
        }

        private void DispatcherOnUnhandledException(DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            this.logger.Fatal(args.Exception);
            this.ShowMessage(args.Exception);
        }

        private void ShowMessage(Exception exception)
        {
            if (this.prevException?.TargetSite == exception?.TargetSite)
            {
                return;
            }

            this.prevException = exception;
            ThreadPool.QueueUserWorkItem(item =>
            {
                UIDispatcher.Invoke(() =>
                {
                    var exceptionWindow = new ExceptionWindow
                    {
                        AllowsTransparency = true,
                        WindowStyle = WindowStyle.None,
                        Exception = exception,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };

                    if (Application.Current.MainWindow != exceptionWindow)
                    {
                        exceptionWindow.Owner = Application.Current.MainWindow;
                    }

                    exceptionWindow.ShowDialog();
                });

                this.prevException = null;
            });
        }

        private void TaskSchedulerOnUnobservedTaskException(UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();
            this.logger.Fatal(args.Exception);
            this.ShowMessage(args.Exception);
        }
    }
}