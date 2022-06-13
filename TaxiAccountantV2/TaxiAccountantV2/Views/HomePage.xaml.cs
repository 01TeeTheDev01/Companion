using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaxiAccountantV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
           
            browserView.Navigated += BrowserView_Navigated;

            browserView.Navigating += BrowserView_Navigating;

            CheckConnectionState();
        }

        private void BrowserView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
        }

        private void BrowserView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            BrowserNavigationAction(e);
        }

        private void BrowserNavigationAction(WebNavigatedEventArgs e)
        {
            var browserNav = e.NavigationEvent;

            activityIndicator.IsVisible = false;
            
            activityIndicator.IsRunning = false;

            switch (browserNav)
            {
                case WebNavigationEvent.Back:
                    DisplayBrowsingButtons((int)WebNavigationEvent.Back);
                    break;
                case WebNavigationEvent.Forward:
                    DisplayBrowsingButtons((int)WebNavigationEvent.Forward);
                    break;
                case WebNavigationEvent.NewPage:
                    DisplayBrowsingButtons((int)WebNavigationEvent.NewPage);
                    break;
                case WebNavigationEvent.Refresh:
                    DisplayBrowsingButtons(0);
                    break;
            }
        }
       
        private async void CheckConnectionState()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await ActivityInProgess();
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        //browserView.Source = new Uri("https://www.enca.com");
                        browserView.Source = new Uri("https://www.power987.co.za");
                    });

                    await Task.Delay(1500);
                    activityIndicator.IsVisible = false;
                }
                else
                    EnableControlls(0);
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", $"Message: {err.Message}", "OK");
            }
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var netAccess = e.NetworkAccess;

            switch (netAccess)
            {
                case NetworkAccess.Unknown:
                    //await DisplayAlert("Network connection", $"Status: {NetworkAccess.Unknown}\n\nPlease check your internet settings and make sure you have enough data bundles.", "OK");
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        SetBroswerSourceToEmpty();
                        EnableControlls((int)NetworkAccess.Unknown);
                    });
                    break;

                case NetworkAccess.None:
                    //await DisplayAlert("Network connection", $"Status: {NetworkAccess.None}\n\nPlease check your internet settings and make sure you have enough data bundles.", "OK");
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        SetBroswerSourceToEmpty();
                        EnableControlls((int)NetworkAccess.None);
                    });
                    break;

                case NetworkAccess.Local:
                    //await DisplayAlert("Network connection", $"Status: {NetworkAccess.Local}\n\nPlease check your internet settings and make sure you have enough data bundles.", "OK");
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        SetBroswerSourceToEmpty();
                        EnableControlls((int)NetworkAccess.Local);
                    });
                    break;

                case NetworkAccess.ConstrainedInternet:
                    //await DisplayAlert("Network connection", $"Status: {NetworkAccess.ConstrainedInternet}\n\nPlease check your internet settings.", "OK");
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        SetBroswerSourceToEmpty();
                        EnableControlls((int)NetworkAccess.ConstrainedInternet);
                    });
                    break;

                case NetworkAccess.Internet:
                    activityIndicator.IsVisible = true;
                    activityIndicator.IsRunning = true;
                    await Task.Delay(2500);
                    browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        browserView.Source = new Uri("https://www.power987.co.za");
                        browserView.IsVisible = true;
                    });
                    await Task.Delay(2000);
                    activityIndicator.IsVisible = false;
                    EnableControlls((int)NetworkAccess.Internet);
                    break;
            }
        }
        
        private void DisplayBrowsingButtons(int result)
        {
            switch (result)
            {
                case 1:
                case 2:
                case 3:
                    buttonsGrid.IsVisible = true;
                    break;

                default:
                    buttonsGrid.IsVisible = false;
                    break;
            }
        }
        
        private async Task ActivityInProgess()
        {
            await Task.Run(() =>
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;
                Task.Delay(new Random().Next(2000,5000));
                //activityIndicator.IsVisible = false;
            });
        }
        
        private void SetBroswerSourceToEmpty()
        {
            browserView.Source = null;
            browserView.Reload();
            browserView.IsVisible = false;
        }
        
        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            if (browserView.CanGoBack)
                browserView.GoBack();
            else
                browserView.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    browserView.Source = new Uri("https://www.power987.co.za");
                    browserView.IsVisible = true;
                });
        }

        private void ForwardBtn_Clicked(object sender, EventArgs e)
        {
            if (browserView.CanGoForward)
                browserView.GoForward();
            else
                browserView.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                browserView.Source = new Uri("https://www.power987.co.za");
                browserView.IsVisible = true;
            });
        }

        private void EnableControlls(int enable)
        {
            if (enable == 4)
            {
                offlineFrame.IsVisible = false;
                activityIndicator.IsVisible = false;
                titleFrame.IsVisible = true;
                buttonsGrid.IsVisible = true;
                browserView.IsVisible = true;
            }
            else
            {
                titleFrame.IsVisible = false;
                buttonsGrid.IsVisible = false;
                offlineFrame.IsVisible = true;
                browserView.IsVisible = false;
            }
        }

        private void LiveStreamBtn_Clicked(object sender, EventArgs e)
        {
            browserView.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                browserView.Source = new Uri("https://www.power987.co.za/stream/#!");
                browserView.IsVisible = true;
            });
        }
    }
}