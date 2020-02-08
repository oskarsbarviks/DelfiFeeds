using Microsoft.Extensions.Configuration;
using Repositories.Constants;
using Repositories.Interfaces;
using Shared.Constants;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    /// <summary>
    /// REpository class for FeedsUpdateTime table
    /// </summary>
    public class FeedsUpdateTimeRepository : IFeedsUpdateTimeRepository
    {
        private IConfiguration _config;
        public FeedsUpdateTimeRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task ChangeFeedsCategoryUpdateTime(string category, DateTime updateDate)
        {
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(ProcedureNameConstants.ChangeFeedCategoryUpdateTime, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UpdateDate", updateDate);
                    cmd.Parameters.AddWithValue("@Category", category);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
