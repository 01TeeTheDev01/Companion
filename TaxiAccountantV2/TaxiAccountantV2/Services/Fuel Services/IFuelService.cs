using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAccountantV2.Services.Fuel
{
    internal interface IFuelService
    {
        Task<List<Models.Fuel>> GetFuel();

        Task<double> CalculateFuelPrice(string amount, decimal price);
    }
}
