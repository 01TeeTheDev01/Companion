using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaxiAccountantV2.Models;

namespace TaxiAccountantV2.Services.Fuel
{
    internal class FuelService : IFuelService
    {
        private readonly List<Models.Fuel> _service;

        public readonly int[] FuelLitres;
        public FuelService()
        {
            _service = new List<Models.Fuel>()
            {
                new Models.Fuel{ Type = FuelType.Unleaded_93.ToString().Replace('_',' '), PricePerLitre = 23.94m },
                new Models.Fuel{ Type = FuelType.Unleaded_95.ToString().Replace('_',' '), PricePerLitre = 24.17m },
                new Models.Fuel{ Type = FuelType.Diesel.ToString(), PricePerLitre = 23.09m }
            };

            FuelLitres = new int[300];

            PrepareLitres();
        }

        public async Task<double> CalculateFuelPrice(string amount, decimal litrePrice)
        {
            var isNumber = double.TryParse(amount, out var fuelLitres);

            if (isNumber)
            {
                var litresReturned = (fuelLitres * (double)litrePrice);

                return await Task.FromResult(litresReturned);
            }

            return default;
        }

        private void PrepareLitres()
        {
            for (int i = 0; i < FuelLitres.Length; i++)
            {
                FuelLitres[i] = i + 1;
            }
        }


        public Task<List<Models.Fuel>> GetFuel()
        {
            return Task.FromResult(_service);
        }
    }
}
