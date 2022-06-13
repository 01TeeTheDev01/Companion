using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TaxiAccountantV2.Models;

namespace TaxiAccountantV2.Services
{
    internal class DataService : IDataConnection
    {
        private readonly SqlConnection sqlConnection;
        private readonly SqlCommand sqlCommand;
        private readonly ILogger logger;

        public DataService(SqlConnection sqlConnection, SqlCommand sqlCommand, ILogger logger)
        {
            this.sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
            this.sqlCommand = sqlCommand ?? throw new ArgumentNullException(nameof(sqlCommand));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<TaxiFare>> GetAllRecords()
        {
            var records = new List<TaxiFare>();

            try
            {
                await sqlConnection.OpenAsync();

                sqlCommand.CommandText = "SELECT * FROM AventureWorks2016.Sales.Sales";

                var collectedRecords = await sqlCommand.ExecuteReaderAsync();

                if (!collectedRecords.HasRows)
                    logger.LogWarning("Unable to load any records. There's nothing to show!");

                sqlConnection.Close();
            }
            catch (SqlException e)
            {
                logger.LogError("#: {0}\nLine: {1}\nMessage: {2}\n\n", e.Number, e.LineNumber, e.Message);
            }

            return await Task.FromResult<IReadOnlyList<TaxiFare>>(records);
        }
    }
}
