using System;
using System.Globalization;
using System.Linq;
using System.Timers;

using TaxiAccountantV2.Models;
using TaxiAccountantV2.Services.TripCalculation;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripPage : TabbedPage
    {
        private OccupantsByType modeOfTransport;
        
        private readonly Trip trip;
        
        private readonly Timer timer;
        
        public TripPage()
        {
            InitializeComponent();
            
            trip = new Trip();

            timer = new Timer()
            {
                Interval = 15000,
                Enabled = true
            };

            timer.Start();

            timer.Elapsed += Timer_Elapsed;

            carPoolRadio.CheckedChanged += CarPoolRadio_CheckedChanged;

            miniBusRadio.CheckedChanged += MiniBusRadio_CheckedChanged;

            busRadio.CheckedChanged += BusRadio_CheckedChanged;
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

        private void CarPoolRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (carPoolRadio.IsChecked)
            {
                miniBusRadio.IsChecked = false;
                busRadio.IsChecked = false;
                modeOfTransport = new OccupantsByType((int)ModeOfTransport.Car_Pool);
                modeOfTransport.SetAmountOfOccupants();
                passengerPicker.ItemsSource = (System.Collections.IList)OccupantsByType.TotalOccupants;
                SetLablesToEmpty();
            }
        }

        private void SetLablesToEmpty()
        {
            amountCollectedEntry.Text = String.Empty;
            priceEntry.Text = String.Empty;
            outstandingAmountLbl.Text = String.Empty;
            changeAmountLbl.Text = String.Empty;
            driversAmountLbl.Text = String.Empty;
        }

        private void MiniBusRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (miniBusRadio.IsChecked)
            {
                carPoolRadio.IsChecked = false;
                busRadio.IsChecked = false;
                modeOfTransport = new OccupantsByType((int)ModeOfTransport.Mini_Bus);
                modeOfTransport.SetAmountOfOccupants();
                passengerPicker.ItemsSource = (System.Collections.IList)OccupantsByType.TotalOccupants;
                SetLablesToEmpty();
            }
        }

        private void BusRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (busRadio.IsChecked)
            {
                miniBusRadio.IsChecked = false;
                carPoolRadio.IsChecked = false;
                modeOfTransport = new OccupantsByType((int)ModeOfTransport.Bus);
                modeOfTransport.SetAmountOfOccupants();
                passengerPicker.ItemsSource = (System.Collections.IList)OccupantsByType.TotalOccupants;
                SetLablesToEmpty();
            }
        }

        private async void CalculateBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var passenger = int.TryParse(passengerPicker.SelectedItem.ToString(), out int passengerResult);

                if (passenger)
                {
                    var price = decimal.TryParse(priceEntry.Text, out decimal pricePerPersonResult);

                    if (price)
                    {
                        var collected = decimal.TryParse(amountCollectedEntry.Text, out decimal amountCollectedResult);

                        if (collected)
                        {
                            var result = trip.CalculateChange(passengerResult, pricePerPersonResult, amountCollectedResult);

                            if (result.Result < 0.0m)
                            {
                                driversAmountLbl.TextColor = Color.Red;
                                outstandingAmountLbl.Text = trip.Message;
                                changeLbl.Text = "OUTSTANDING AMOUNT";
                            }
                            else
                            {
                                driversAmountLbl.TextColor = Color.Green;
                                changeLbl.Text = "CHANGE";
                                outstandingAmountLbl.Text = string.Empty;
                            }

                            driversAmountLbl.Text = trip.DriverAmount;
                            changeAmountLbl.Text = trip.ChangeAmount;
                        }
                    }
                    else
                        await DisplayAlert("Error", $"Message: Number format is invalid. Enter {price} as '{price.ToString().Replace('.', ',')}'.", "OK");
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", $"Message: {err.Message}", "OK");
            }
        }

        private void AmountCollectedEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearLabels();
        }

        private void PriceEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearLabels();
        }

        private void ClearLabels()
        {
            driversAmountLbl.Text = string.Empty;
            outstandingAmountLbl.Text = string.Empty;
            changeAmountLbl.Text = string.Empty;
        }
    }
}