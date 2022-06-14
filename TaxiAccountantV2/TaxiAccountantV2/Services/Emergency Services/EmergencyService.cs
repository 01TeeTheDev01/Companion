using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TaxiAccountantV2.Models.Emergency_Models;

namespace TaxiAccountantV2.Services.Emergency_Services
{
    public class EmergencyService : IEmergencyService
    {
        public readonly List<EmergencyModel> emergencies;
        public EmergencyService()
        {
            emergencies = new List<EmergencyModel>()
            { 
                new EmergencyModel{ ServiceName = "ANY EMERGENCY", ServiceNumber = "112" },
                new EmergencyModel{ ServiceName = "NETCARE 911", ServiceNumber = "082 911" },
                new EmergencyModel{ ServiceName = "AMBULANCE", ServiceNumber = "10177" },
                new EmergencyModel{ ServiceName = "FIRE DEPARTMENT", ServiceNumber = "10177" },
                new EmergencyModel{ ServiceName = "ER24 ", ServiceNumber = "084 124" },
                new EmergencyModel{ ServiceName = "MOUNTAIN RESCUE – KZN", ServiceNumber = "031 307 7744" },
                new EmergencyModel{ ServiceName = "MOUNTAIN RESCUE – WESTERN CAPE", ServiceNumber = "021 948 9900" },
                new EmergencyModel{ ServiceName = "MOUNTAIN RESCUE – GAUTENG", ServiceNumber = "074 125 1385 / 074 163 3952" },
                new EmergencyModel{ ServiceName = "LIFE LINE", ServiceNumber = "0861 322 322" },
                new EmergencyModel{ ServiceName = "SUICIDE CRISIS LINE", ServiceNumber = "0800 567 567" },
                new EmergencyModel{ ServiceName = "SADAG MENTAL HEALTH LINE", ServiceNumber = "(011) 234 4837" },
                new EmergencyModel{ ServiceName = "CHILD LINE", ServiceNumber = "0800 05 55 55" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - GAUTENG", ServiceNumber = "0800 203 886" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - EASTERN CAPE", ServiceNumber = "0800 032 364" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - FREE STATE", ServiceNumber = "0800 535 554" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - KZN", ServiceNumber = "033 395 2009" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - LIMPOPO", ServiceNumber = "0800 919 191" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - MPUMALANGA", ServiceNumber = "0800 204 098" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - NORTHERN CAPE", ServiceNumber = "018 387 5778" },
                new EmergencyModel{ ServiceName = "HEALTH & ACCIDENTS - WESTERN CAPE", ServiceNumber = "021 483 5624" }
            };
        }
        public Task<IEnumerable<EmergencyModel>> GetEmergencyServices()
        {
            throw new NotImplementedException();
        }
    }
}
