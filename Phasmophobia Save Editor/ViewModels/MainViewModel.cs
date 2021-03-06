﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PhasmophobiaSaveEditor.Controls.Dialog;
using PhasmophobiaSaveEditor.Infrastructures;
using PhasmophobiaSaveEditor.Models;
using PhasmophobiaSaveEditor.Services;
using PhasmophobiaSaveEditor.Utils;
using PhasmophobiaSaveEditor.ViewModels.Base;
using PhasmophobiaSaveEditor.Views;

namespace PhasmophobiaSaveEditor.ViewModels
{
    internal class MainViewModel : BaseBindable, IWindowViewModel
    {
        private readonly List<string> itemsList = new List<string>
        {
            "EMFReaderInventory",
            "FlashlightInventory",
            "CameraInventory",
            "LighterInventory",
            "CandleInventory",
            "UVFlashlightInventory",
            "CrucifixInventory",
            "DSLRCameraInventory",
            "EVPRecorderInventory",
            "SaltInventory",
            "SageInventory",
            "TripodInventory",
            "StrongFlashlightInventory",
            "MotionSensorInventory",
            "SoundSensorInventory",
            "SanityPillsInventory",
            "ThermometerInventory",
            "GhostWritingBookInventory",
            "IRLightSensorInventory",
            "ParabolicMicrophoneInventory",
            "GlowstickInventory",
            "HeadMountedCameraInventory"
        };

        private readonly SaveService saveService;
        private List<object> editableSaveProperties;
        private PhasmophobiaSave phasmophobiaSave;
        private Theme theme = Theme.Light;

        public MainViewModel()
        {
            this.saveService = new SaveService(PathUtil.SaveFile);
            this.SaveCommand = new RelayCommand(this.SaveCommandExecute);
            this.ReloadCommand = new RelayCommand(this.ReloadCommandExecute);
            this.OpenAboutCommand = new RelayCommand(this.OpenAboutCommandExecute);
            this.SetAllItemsCommand = new RelayCommand(this.SetAllItemsCommandExecute);
            this.DebugSaveCommand = new RelayCommand(this.DebugSaveCommandExecute);
            HotKey.Register(Key.D, KeyModifier.Ctrl | KeyModifier.Shift, DebugSaveCommand);
        }

        private void DebugSaveCommandExecute()
        {
            MessageDialog.ShowDialog(DialogParameters.Success(this.saveService.ReadRaw()));
        }

        public ICommand DebugSaveCommand { get; }

        public ICommand OpenAboutCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SetAllItemsCommand { get; }

        public List<object> EditableSaveProperties
        {
            get => this.editableSaveProperties;
            private set => this.Set(ref this.editableSaveProperties, value);
        }

        public Language Language
        {
            get => LocalizationManager.Current.Language;
            set
            {
                if (LocalizationManager.Current.SetLanguage(value))
                {
                    this.RaisePropertyChanged();
                }
            }
        }

        public PhasmophobiaSave PhasmophobiaSave
        {
            get => this.phasmophobiaSave;
            set => this.Set(ref this.phasmophobiaSave, value);
        }

        public Theme Theme
        {
            get => this.theme;
            set
            {
                if (this.Set(ref this.theme, value))
                {
                    ThemeManager.ChangeTheme(value);
                }
            }
        }

        public void Init()
        {
            this.Load();
        }

        public void OnLoaded(Window window)
        {
            this.Load();
        }

        private void Load()
        {
            if (!File.Exists(this.saveService.Filename))
            {
                MessageDialog.ShowDialog(DialogParameters.Error(LocalizationManager.GetLocalizationString("Main.SaveNotExist")));
                Application.Current.Shutdown();
            }

            if (!this.saveService.TryRead(out var save))
            {
                MessageDialog.ShowDialog(DialogParameters.Error(LocalizationManager.GetLocalizationString("Main.SaveCorrupted")));
                Application.Current.Shutdown();
            }

            this.PhasmophobiaSave = save;

            this.EditableSaveProperties = new List<object>
            {
                new EditableSaveStringProperty(this.PhasmophobiaSave.StringData.FirstOrDefault(x => x.Key.Equals("GhostType", StringComparison.InvariantCultureIgnoreCase)), true),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("myTotalExp", StringComparison.InvariantCultureIgnoreCase)), 0, 999999, 10),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("PlayersMoney", StringComparison.InvariantCultureIgnoreCase)), 0, 999999, 10),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("EMFReaderInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("FlashlightInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("CameraInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("LighterInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("CandleInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("UVFlashlightInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("CrucifixInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("DSLRCameraInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("EVPRecorderInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("SaltInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("SageInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("TripodInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("StrongFlashlightInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("MotionSensorInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("SoundSensorInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("SanityPillsInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("ThermometerInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("GhostWritingBookInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("IRLightSensorInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("ParabolicMicrophoneInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("GlowstickInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3),
                new EditableSaveIntProperty(this.PhasmophobiaSave.IntData.FirstOrDefault(x => x.Key.Equals("HeadMountedCameraInventory", StringComparison.InvariantCultureIgnoreCase)), 0, 999, 3)
            };
        }

        private void OpenAboutCommandExecute()
        {
            MessageDialog.ShowDialog(new DialogParameters
            {
                Content = new AboutView(),
                DialogStartupLocation = WindowStartupLocation.CenterOwner,
                Button = DialogButton.OK
            });
        }

        private void ReloadCommandExecute()
        {
            this.Load();
        }

        private void SaveCommandExecute()
        {
            this.saveService.Save(this.PhasmophobiaSave);
            MessageDialog.ShowDialog(DialogParameters.Success(LocalizationManager.GetLocalizationString("Main.Saved")));
        }

        private void SetAllItemsCommandExecute()
        {
            var vm = new SetAllItemsViewModel();
            var result = MessageDialog.ShowDialog(new DialogParameters
            {
                Button = DialogButton.OKCancel,
                Content = new SetAllItemsView(vm),
                DialogStartupLocation = WindowStartupLocation.CenterOwner,
                Header = LocalizationManager.GetLocalizationString("Main.SetAllItems")
            });

            if (result == true)
            {
                this.itemsList.ForEach(x =>
                {
                    var saveProperty = this.EditableSaveProperties.OfType<EditableSaveProperty<int>>().First(xx => xx.Data.Key == x);
                    saveProperty.Data.Value = vm.Value;
                });
            }
        }
    }
}