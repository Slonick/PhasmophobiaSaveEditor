using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using PhasmophobiaSaveEditor.Attributes;
using PhasmophobiaSaveEditor.Extensions;
using PhasmophobiaSaveEditor.Infrastructures;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.Utils
{
    public class LocalizationManager : BaseBindable
    {
        private static readonly object SyncObject = new object();
        private static volatile LocalizationManager current;

        private Language language;

        private LocalizationManager() { }

        public static LocalizationManager Current
        {
            get
            {
                if (current == null)
                {
                    lock (SyncObject)
                    {
                        if (current == null)
                        {
                            current = new LocalizationManager();
                        }
                    }
                }

                return current;
            }
        }

        public Language Language
        {
            get => this.language;
            private set
            {
                this.language = value;
                this.ChangeLanguage(value);
                this.RaisePropertyChanged();
            }
        }

        public void ChangeLanguage(Language lang)
        {
            var cultureName = lang.GetAttributeOfType<CultureNameAttribute>()?.Suffix;
            if (string.IsNullOrWhiteSpace(cultureName))
            {
                return;
            }

            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureName);

            const string pattern = @"/Localization/";
            var resources = Application.Current.Resources.MergedDictionaries.Where(d => d.Source?.OriginalString.Contains(pattern) ?? false);
            resources.ToList().ForEach(resourceDictionary =>
            {
                var basePath = resourceDictionary.Source.OriginalString.Substring(0, resourceDictionary.Source.OriginalString.IndexOf(pattern, StringComparison.Ordinal));
                var newRes = new ResourceDictionary
                {
                    Source = new Uri($"{basePath}{pattern}lang.{cultureName}.xaml", UriKind.Relative)
                };

                var index = Application.Current.Resources.MergedDictionaries.IndexOf(resourceDictionary);
                Application.Current.Resources.MergedDictionaries[index] = newRes;
            });
        }

        public bool SetLanguage(Language lang)
        {
            if (this.Language == lang)
            {
                return false;
            }

            this.Language = lang;
            this.ChangeLanguage(lang);
            return true;
        }

        public static string GetLocalizationString(string key)
            => Application.Current.Resources.Contains(key) ? Application.Current.Resources[key] as string : key;
    }
}