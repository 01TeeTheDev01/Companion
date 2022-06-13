using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using TaxiAccountantV2.Models.News;

namespace TaxiAccountantV2.Services.Updates
{
    internal interface INewsUpdateService
    {
        Task<IReadOnlyList<NewsUpdate>> GetNewsUpdates();
        Task<IReadOnlyList<NewsUpdate>> GetStrikeNewsUpdates();
        Task<IReadOnlyList<NewsUpdate>> GetTarrifNewsUpdates();
    }
}
