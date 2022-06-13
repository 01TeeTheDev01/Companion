using System;
using TaxiAccountantV2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            MainPage = new SplashPage();
            //MainPage = new TripPage();
            //MainPage = new CarCalculatorPage();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            MainPage = new MainPage();
        }
    }
}
