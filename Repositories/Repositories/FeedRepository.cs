using Microsoft.Extensions.Configuration;
using Repositories.Constants;
using Repositories.DataObject;
using Repositories.Enums;
using Repositories.Interfaces;
using Shared.Constants;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    /// <summary>
    /// REpository class for "Feeds" table
    /// </summary>
    public class FeedRepository : IFeedRepository
    {
        private IConfiguration _config;
        public FeedRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> UpdateOrInsertFeeds(List<DelfiFeedDataObject> feeds)
        {
            var hasNewRecordsAddedToCategory = false;
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                foreach (var feed in feeds)
                {
                    var isNewRecord = await ExecuteSqlCommand(feed, connection);
                    hasNewRecordsAddedToCategory = hasNewRecordsAddedToCategory || isNewRecord;
                }
            }

            return hasNewRecordsAddedToCategory;
        }

        private async Task<bool> ExecuteSqlCommand(DelfiFeedDataObject feed, SqlConnection connection)
        {
            using (var cmd = new SqlCommand(ProcedureNameConstants.InsertOrUpdateFeedByID, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", feed.ID);
                cmd.Parameters.AddWithValue("@Link", feed.Link);
                cmd.Parameters.AddWithValue("@Title", feed.Title);
                cmd.Parameters.AddWithValue("@Description", feed.Description);
                cmd.Parameters.AddWithValue("@CommentCount", feed.CommentCount);
                cmd.Parameters.AddWithValue("@PictureUrl", feed.PictureUrl.AbsoluteUri);
                cmd.Parameters.AddWithValue("@PublishDate", feed.PublishDate);
                cmd.Parameters.AddWithValue("@Category", feed.Category);

                return (int)await cmd.ExecuteScalarAsync() == (int)FeedAge.New ? true : false;
            }
        }
    }
}
