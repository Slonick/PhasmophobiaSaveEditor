using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using PhasmophobiaSaveEditor.Converters;
using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.MarkupExtensions
{
    public class LocalizationBinding : MarkupExtension
    {
        public LocalizationBinding() { }

        public LocalizationBinding(PropertyPath path)
        {
            this.Path = path;
        }

        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => new MultiBinding
            {
                Converter = new LocalizationConverter(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Bindings =
                {
                    new Binding(this.Path.Path)
                    {
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        Mode = BindingMode.OneWay
                    },
                    new Binding(nameof(LocalizationManager.Language))
                    {
                        Source = LocalizationManager.Current,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        Mode = BindingMode.OneWay
                    }
                }
            };
    }
}