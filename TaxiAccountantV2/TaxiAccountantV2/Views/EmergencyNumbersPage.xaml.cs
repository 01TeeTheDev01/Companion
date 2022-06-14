using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using TaxiAccountantV2.Models.Emergency_Models;
using TaxiAccountantV2.Services.Emergency_Services;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencyNumbersPage : TabbedPage
    {
        private readonly EmergencyService emergencyService;

        private readonly Timer timer;
        public EmergencyNumbersPage()
        {
            InitializeComponent();

            emergencyService = new EmergencyService();

            LoadListItems();

            timer = new Timer()
            {
                Interval = 15000,
                Enabled = true
            };

            timer.Start();

            timer.Elapsed += Timer_Elapsed;
        }

        private void LoadListItems()
        {
            foreach (var emergencyItem in emergencyService.emergencies.OrderBy(s => s.ServiceName))
                EmergencyListView.Items.Add(emergencyItem.ServiceName);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LocationLbl.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var result = await Geolocation.GetLocationAsync();

                    if (result != null)
                    {
                        var location = await Geocoding.GetPlacemarksAsync(result);

                        if (location.ElementAt(0).PostalCode != string.Empty)
                        {
                            LocationLbl.Text = $"{location.ElementAt(0).SubThoroughfare}, " +
                                               $"{location.ElementAt(0).Thoroughfare}, " +
                                               $"{location.ElementAt(0).Locality}, " +
                                               $"{location.ElementAt(0).PostalCode}, " +
                                               $"{location.ElementAt(0).SubAdminArea}, " +
                                               $"{location.ElementAt(0).AdminArea}, " +
                                               $"{location.ElementAt(0).CountryName}";
                        }
                        else
                            LocationLbl.Text = $"{location.ElementAt(0).SubThoroughfare}, " +
                                               $"{location.ElementAt(0).Thoroughfare}, " +
                                               $"{location.ElementAt(0).Locality}, " +
                                               $"{location.ElementAt(0).SubAdminArea}, " +
                                               $"{location.ElementAt(0).AdminArea}, " +
                                               $"{location.ElementAt(0).CountryName}";
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"{ex.Message}", "OK");
                    return;
                }
            });
        }

        private void CallBtn_Clicked(object sender, EventArgs e)
        {
            CallService();
        }

        private void CallService()
        {
            for (int i = 0; i < emergencyService.emergencies.OrderBy(s => s.ServiceName).Count(); i++)
            {
                if (i == EmergencyListView.SelectedIndex)
                    PlaceCall(emergencyService.emergencies.OrderBy(s => s.ServiceName).ElementAt(i));
            }
        }

        private async void PlaceCall(EmergencyModel emergencyObject)
        {
            if (!string.IsNullOrWhiteSpace(emergencyObject.ServiceName) && !string.IsNullOrEmpty(emergencyObject.ServiceName))
            {
                try 
                {
                    var result = await DisplayActionSheet($"Place call to {emergencyObject.ServiceName} - {emergencyObject.ServiceNumber}?", string.Empty, string.Empty,"YES", "NO");
                    
                    if (result.Equals("YES"))
                    {
                        PhoneDialer.Open(emergencyObject.ServiceNumber);
                        EmergencyListView.SelectedIndex = -1;
                    }

                    EmergencyListView.SelectedIndex = -1;
                }
                catch (Exception err)
                {
                    await DisplayAlert("Error", $"{err.Message}", "OK");
                    EmergencyListView.SelectedIndex = -1;
                }
            }
        }
    }
}