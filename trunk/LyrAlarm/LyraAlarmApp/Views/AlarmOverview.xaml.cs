using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using LyrAlarm.Shared;

namespace LyraAlarmApp.Views
{
    public sealed partial class AlarmOverview : Page
    {
        public ObservableCollection<AlarmViewModel> Alarms { get; set; }

        public AlarmOverview()
        {
            this.InitializeComponent();
            this.Alarms = new ObservableCollection<AlarmViewModel>();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            this.RefreshAlarms();
        }

        private async void RefreshAlarms()
        {
            StatusBarProgressIndicator progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            progressbar.Text = "Loading Alarms";
            await progressbar.ShowAsync();

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(new Uri("https://timeprovider.azurewebsites.net/Api.ashx?action=get-alarms"));

            this.Alarms.Clear();
            var alarms = JsonConvert.DeserializeObject<List<AlarmViewModel>>(response);

            foreach (var alarm in alarms)
            {
                this.Alarms.Add(alarm);
            }

            this.AlarmsListView.ItemsSource = this.Alarms;

            await progressbar.HideAsync();
        }

        private async void AlarmsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditAlarm editView = new EditAlarm((AlarmViewModel)e.ClickedItem);

            await editView.ShowAsync();

            this.AlarmsListView.ItemsSource = this.Alarms;
        }
    }
}
