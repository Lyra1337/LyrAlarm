using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LyraAlarmApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LyraAlarmApp.Views
{
    public sealed partial class EditAlarm : ContentDialog
    {
        public AlarmViewModel Alarm { get; private set; }

        public EditAlarm(AlarmViewModel alarm)
        {
            this.InitializeComponent();
            this.Alarm = alarm;

            this.timePicker.Time = TimeSpan.FromMinutes(alarm.Time);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Alarm.Time = (int)this.timePicker.Time.TotalMinutes;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}