using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAccountantV2.Services.Emergency_Services
{
    internal interface IEmergencyService
    {
        Task<IEnumerable<Models.Emergency_Models.EmergencyModel>> GetEmergencyServices();
    }
}
