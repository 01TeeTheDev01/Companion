using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TaxiAccountantV2.Models;

namespace TaxiAccountantV2.Services
{
    internal interface IDataConnection
    {
        Task<IReadOnlyList<TaxiFare>> GetAllRecords(); 
    }
}
