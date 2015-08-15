using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
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
using Windows.Web.Http;
using LyrAlarm.Shared;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LyraAlarmApp.Views
{
    public sealed partial class EditAlarm : ContentDialog, INotifyPropertyChanged
    {
        private bool Monday
        {
            get
            {
                return this.Alarm.Monday;
            }
            set
            {
                this.Alarm.Monday = value;
                this.NotifyPropertyChanged();
            }
        }

        public AlarmViewModel Alarm { get; private set; }

        public EditAlarm(AlarmViewModel alarm)
        {
            this.InitializeComponent();
            this.Alarm = alarm;

            this.DataContext = alarm;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string json = JsonConvert.SerializeObject(this.Alarm);

            HttpClient client = new HttpClient();

            HttpMultipartFormDataContent data = new HttpMultipartFormDataContent();

            data.Add(new HttpStringContent(json), "alarm");

            var response = await client.PostAsync(new Uri("https://timeprovider.azurewebsites.net/Api.ashx?action=set-alarm"), data);

            response.ToString();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}