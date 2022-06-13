using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiAccountantV2.Models;
using TaxiAccountantV2.Services.Updates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationSettingsPage : TabbedPage
    {
        private readonly NewsUpdateService _newsUpdateService;

        public NotificationSettingsPage()
        {
            InitializeComponent();

            _newsUpdateService = new NewsUpdateService();       
        }

        private async void SubscribeBtn_Clicked(object sender, EventArgs e)
        {
            if (TariffSwitch.On && StrikeSwitch.On)
                NewsList.ItemsSource = await _newsUpdateService.GetNewsUpdates();
            else if (TariffSwitch.On)
            {
                var data = await _newsUpdateService.GetTarrifNewsUpdates();

                if (data == null || data.Count == 0)
                    await DisplayAlert("Tariff notification", "No official taxi fee increase to report!","Okay");

                NewsList.ItemsSource = data;


            }
            else if (StrikeSwitch.On)
            {
                var data = await _newsUpdateService.GetStrikeNewsUpdates();

                if (data == null || data.Count == 0)
                    await DisplayAlert("Strike notification", "No official strike to report!", "Okay");

                NewsList.ItemsSource = data;
            }
            else
                NewsList.ItemsSource = null;
        }
    }
}