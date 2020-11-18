using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.ViewModels
{
    public class AboutViewModel : BaseBindable
    {
        public AboutViewModel()
        {
            this.OpenLinkCommand = new RelayCommand<string>(this.OpenLinkCommandExecute);
        }

        public ICommand OpenLinkCommand { get; }

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;


        private void OpenLinkCommandExecute(string link)
        {
            Process.Start(link);
        }
    }
}