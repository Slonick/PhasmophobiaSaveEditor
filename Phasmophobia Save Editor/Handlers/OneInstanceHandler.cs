using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using PhasmophobiaSaveEditor.Extensions;

namespace PhasmophobiaSaveEditor.Handlers
{
    public class OneInstanceHandler
    {
        private static OneInstanceHandler current;

        private readonly Mutex mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetAssemblyAttribute<GuidAttribute>().Value);

        public static OneInstanceHandler Current => current ?? (current = new OneInstanceHandler());

        public void RegisterHandler()
        {
            if (!this.mutex.WaitOne(TimeSpan.Zero, true))
            {
                Environment.Exit(0);
            }

            Application.Current.Exit += (sender, args) => this.mutex.ReleaseMutex();
        }
    }
}