using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiAccountantV2.Models;
using TaxiAccountantV2.Services;
using Xamarin.Forms;

namespace TaxiAccountantV2
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapButton_Clicked(object sender, EventArgs e)
        {
            //IDataConnection dataConnection = new DataService(new System.Data.SqlClient.SqlConnection(DataConnection.connectionString),
            //                                                 new System.Data.SqlClient.SqlCommand(), null);
            //var result = await dataConnection.GetAllRecords();

            //if (result.Count == 0 || result == null)
            //{
            //    await DisplayAlert("Result","No data to display!", "OK");
            //}
        }
    }
}
