using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using TaxiAccountantV2.Models;
using TaxiAccountantV2.Services.Fuel;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FuelPage : TabbedPage
    {
        private readonly FuelService service;

        private readonly Timer timer;

        public FuelPage()
        {
            InitializeComponent();

            service = new FuelService();

            timer = new Timer()
            { 
                Interval = 15000, 
                Enabled = true
            };

            timer.Start();

            timer.Elapsed += Timer_Elapsed;

            GetFuelTypes();

            fuelCapacity.ItemsSource = service.FuelLitres;
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

        private async void GetFuelTypes()
        {
            foreach (var item in await service.GetFuel())
                fuelPicker.Items.Add(item.Type);
        }

        private void FuelPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            PricePerLitre(sender);
        }

        private void PricePerLitre(object sender)
        {
            if (sender is Picker picker)
            {
                LitresLbl.Text = string.Empty;

                fuelCapacity.SelectedIndex = -1;

                switch (picker.SelectedIndex)
                {
                    case (int)FuelType.Unleaded_93:
                        pricePerLitreLbl.Text = string.Format("R {0:c2}", service.GetFuel().Result[0].PricePerLitre.ToString(new CultureInfo("en-ZA")));
                        break;

                    case (int)FuelType.Unleaded_95:
                        pricePerLitreLbl.Text = string.Format("R {0:c2}", service.GetFuel().Result[1].PricePerLitre.ToString(new CultureInfo("en-ZA")));
                        break;

                    case (int)FuelType.Diesel:
                        pricePerLitreLbl.Text = string.Format("R {0:c2}", service.GetFuel().Result[2].PricePerLitre.ToString(new CultureInfo("en-ZA")));
                        break;
                }
            }
        }

        private async void CalculateBtn_Clicked(object sender, EventArgs e)
        {
            double priceOfFuel;

            switch (fuelPicker.SelectedIndex)
            {
                case 0:
                    if (decimal.Parse(fuelCapacity.SelectedItem.ToString()) != -1)
                    {
                        priceOfFuel = await service.CalculateFuelPrice(fuelCapacity.SelectedItem.ToString(), service.GetFuel().Result[0].PricePerLitre);

                        var currency = priceOfFuel.ToString(new CultureInfo("en-ZA"));

                        LitresLbl.Text = $"R {string.Format("{0:c2}", currency)}";
                    }
                    else
                        await DisplayAlert("Error", "Your litre capacity is less than the fuel price per litre.", "OK");
                    break;

                case 1:
                    if(decimal.Parse(fuelCapacity.SelectedItem.ToString()) != -1)
                    {
                        priceOfFuel = await service.CalculateFuelPrice(fuelCapacity.SelectedItem.ToString(), service.GetFuel().Result[1].PricePerLitre);

                        var currency = priceOfFuel.ToString(new CultureInfo("en-ZA"));

                        LitresLbl.Text = $"R {string.Format("{0:c2}", currency)}";
                    }
                    else
                        await DisplayAlert("Error", "Your litre capacity is less than the fuel price per litre.", "OK");
                    break;

                case 2:
                    if(decimal.Parse(fuelCapacity.SelectedItem.ToString()) != -1)
                    {
                        priceOfFuel = await service.CalculateFuelPrice(fuelCapacity.SelectedItem.ToString(), service.GetFuel().Result[2].PricePerLitre);

                        var currency = priceOfFuel.ToString(new CultureInfo("en-ZA"));

                        LitresLbl.Text = $"R {string.Format("{0:c2}", currency)}";
                    }
                    else
                        await DisplayAlert("Error", "Your litre capacity is less than the fuel price per litre.", "OK");
                    break;

                default:
                    await DisplayAlert("Error", "Select a fuel type.", "OK");
                    break;
            }
        }

        private void AmountEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (amountEntry.Text == string.Empty)
            //    amountEntry.Text = "0lt";
        }
    }
}