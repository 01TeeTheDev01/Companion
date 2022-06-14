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
                EmergencyServicePicker.Items.Add(emergencyItem.ServiceName);
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

        private async void CallService()
        {
            if (EmergencyServicePicker.SelectedIndex != -1)
                for (int i = 0; i < emergencyService.emergencies.OrderBy(s => s.ServiceName).Count(); i++)
                {
                    if (i == EmergencyServicePicker.SelectedIndex)
                        PlaceCall(emergencyService.emergencies.OrderBy(s => s.ServiceName).ElementAt(i));
                }
            else
                await DisplayAlert("Error", "Message: Please select a service to call.", "OK");
        }

        private async void PlaceCall(EmergencyModel emergencyObject)
        {
            if (emergencyObject != null)
            {
                try
                {
                    var result = await DisplayActionSheet(title: $"Place call to {emergencyObject.ServiceName} - {emergencyObject.ServiceNumber}?", string.Empty, string.Empty, "YES", "NO");

                    if (result.Equals("YES"))
                    {
                        PhoneDialer.Open(emergencyObject.ServiceNumber);

                        EmergencyServicePicker.SelectedIndex = -1;
                    }

                    EmergencyServicePicker.SelectedIndex = -1;
                }
                catch (Exception err)
                {
                    await DisplayAlert("Error", $"{err.Message}", "OK");
                    EmergencyServicePicker.SelectedIndex = -1;
                }
            }
        }
    }
}