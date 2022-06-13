using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaxiAccountantV2.Models.News;

namespace TaxiAccountantV2.Services.Updates
{
    internal class NewsUpdateService : INewsUpdateService
    {
        private readonly List<NewsUpdate> NewsUpdates;

        public NewsUpdateService()
        {
            NewsUpdates = new List<NewsUpdate>
            {
                new NewsUpdate
                {
                    NewsText = "SANTACO taxi fare increase on the cards",
                    NewsNameType = NewsType.Tariff.ToString(),
                    NewsType = (int)NewsType.Tariff
                },

                new NewsUpdate
                {
                    NewsText = $"VTA strike {DateTime.Today.AddDays(14):D}",
                    NewsNameType = NewsType.Strike.ToString(),
                    NewsType = (int)NewsType.Strike
                },

                new NewsUpdate
                {
                    NewsText = "ATA taxi fare increase sparks outrage",
                    NewsNameType = NewsType.Tariff.ToString(),
                    NewsType = (int)NewsType.Tariff
                },

                new NewsUpdate
                {
                    NewsText = $"NANDUWA strike {DateTime.Today.AddDays(9):D}",
                    NewsNameType = NewsType.Strike.ToString(),
                    NewsType = (int)NewsType.Strike
                }
            };
        }

        public async Task<IReadOnlyList<NewsUpdate>> GetNewsUpdates()
        {
            return await Task.FromResult((IReadOnlyList<NewsUpdate>)NewsUpdates);
        }

        public async Task<IReadOnlyList<NewsUpdate>> GetTarrifNewsUpdates()
        {
            var tarrifResult = NewsUpdates.Where(news => news.NewsType == (int)NewsType.Tariff).ToList();

            if (!tarrifResult.Any())
                return null;

            return await Task.FromResult((IReadOnlyList<NewsUpdate>)tarrifResult);
        }

        public async Task<IReadOnlyList<NewsUpdate>> GetStrikeNewsUpdates()
        {
            var strikeResult = NewsUpdates.Where(c => c.NewsType == (int)NewsType.Strike).ToList();

            if (!strikeResult.Any())
                return null;

            return await Task.FromResult((IReadOnlyList<NewsUpdate>)strikeResult);
        }
    }
}
