using System.Threading.Tasks;

namespace TaxiAccountantV2.Services.TripCalculation
{
    interface ITrip
    {
        Task<decimal> CalculateChange(int passengers, decimal price, decimal amountCollected);
    }
}
