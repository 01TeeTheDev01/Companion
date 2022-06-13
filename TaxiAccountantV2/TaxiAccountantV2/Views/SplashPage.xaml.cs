using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();

            LoadSplash();
        }

        private async void LoadSplash()
        {
            await LogoAnimation();
        }

        public async Task LogoAnimation()
        {
            textLogo.Opacity = 0;

            for (int i = 0; i < 5; i++)
            {

                await textLogo.FadeTo(1, 1200, Easing.SinInOut);

                await Task.Delay(1500);

                await textLogo.FadeTo(0, 1200, Easing.SinInOut);
            }

            await carImage.FadeTo(0, 1200, Easing.SinInOut);
            //await Task.Delay(3000);

            Application.Current.MainPage = new MainPage();
        }
    }
}