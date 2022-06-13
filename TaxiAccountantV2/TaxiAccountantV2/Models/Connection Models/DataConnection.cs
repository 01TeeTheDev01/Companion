using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using Xamarin.Forms;

namespace TaxiAccountantV2.Models.Connection
{
    internal static class DataConnection
    {
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
    }
}
