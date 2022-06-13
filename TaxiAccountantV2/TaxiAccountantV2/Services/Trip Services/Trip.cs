using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAccountantV2.Services.TripCalculation
{
    public class Trip : ITrip
    {
        private decimal driverAmount, outstandingAmount;

        public string Message { get; private set; }
        public string DriverAmount 
        { 
            get 
            { 
                return string.Format("R {0:c2}", driverAmount.ToString(CultureInfo.GetCultureInfo("en-ZA"))); 
            }

            set 
            { 
                driverAmount = Convert.ToDecimal(value); 
            }
        }
        public string ChangeAmount 
        { 
            get 
            { 
                return string.Format("R {0:c2}", outstandingAmount.ToString(CultureInfo.GetCultureInfo("en-ZA")));
            }

            set 
            { 
                outstandingAmount = Convert.ToDecimal(value); 
            }
        }

        public Task<decimal> CalculateChange(int passengers, decimal price, decimal amountCollected)
        {
            driverAmount = passengers * price;

            outstandingAmount = amountCollected - driverAmount;

            if (outstandingAmount < 0)
                Message = $"The driver's money is short by {string.Format("R {0:c2}", outstandingAmount.ToString(CultureInfo.GetCultureInfo("en-ZA")))}";

            return Task.FromResult(outstandingAmount);
        }
    }
}