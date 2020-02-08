using Microsoft.Extensions.Configuration;
using Repositories.Constants;
using Repositories.DataObject;
using Repositories.Enums;
using Repositories.Interfaces;
using Shared.Constants;
using System;
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
                    var isNewRecord = await ExecuteFeedInsertCommand(feed, connection);
                    hasNewRecordsAddedToCategory = hasNewRecordsAddedToCategory || isNewRecord;
                }
            }

            return hasNewRecordsAddedToCategory;
        }

        public async Task<List<DelfiFeedDataObject>> GetFeedsByUserID(string userID, string defaultCattegory, int defaultCount)
        {
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var settings = await ExecuteGetFeedSettingsCommand(userID, connection);
                if (settings == null)
                {
                    return await ExecuteGetFeedsByCategoryCommand(defaultCattegory, defaultCount, connection);
                }
                else
                {
                    return await ExecuteGetFeedsByCategoryCommand(settings.Category, settings.FeedCount, connection);
                }
            }
        }

        private async Task<List<DelfiFeedDataObject>> ExecuteGetFeedsByCategoryCommand(string category, int count, SqlConnection connection)
        {
            using (var cmd = new SqlCommand(ProcedureNameConstants.GetFeedsByCategory, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Count", count);
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    var feedList = new List<DelfiFeedDataObject>();
                    while (await reader.ReadAsync())
                    {
                        feedList.Add(new DelfiFeedDataObject
                        {
                            ID = reader.GetString(reader.GetOrdinal(FeedsTableColumNameConstants.ID)),
                            CommentCount = reader.GetInt32(reader.GetOrdinal(FeedsTableColumNameConstants.CommentCount)),
                            Description = reader.GetString(reader.GetOrdinal(FeedsTableColumNameConstants.Description)),
                            Link = reader.GetString(reader.GetOrdinal(FeedsTableColumNameConstants.Link)),
                            PictureUrl = new Uri(reader.GetString(reader.GetOrdinal(FeedsTableColumNameConstants.PictureUrl))),
                            PublishDate = reader.GetDateTime(reader.GetOrdinal(FeedsTableColumNameConstants.PublishDate)),
                            Title = reader.GetString(reader.GetOrdinal(FeedsTableColumNameConstants.Title))
                        });
                    }

                    return feedList;
                }
                else
                {
                    return new List<DelfiFeedDataObject>();
                }
            }
        }

        private async Task<FeedSettingsDataObject> ExecuteGetFeedSettingsCommand(string userID, SqlConnection connection)
        {
            using (var cmd = new SqlCommand(ProcedureNameConstants.GetFeedSettingsByUserID, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    return new FeedSettingsDataObject
                    {
                        UserID = reader.GetString(reader.GetOrdinal(FeedsSettingsTableColumnConstants.UserID)),
                        Category = reader.GetString(reader.GetOrdinal(FeedsSettingsTableColumnConstants.Category)),
                        FeedCount = reader.GetInt32(reader.GetOrdinal(FeedsSettingsTableColumnConstants.FeedCount))
                    };
                }
                else
                {
                    await reader.CloseAsync();
                    return null;
                }
            }
        }

        private async Task<bool> ExecuteFeedInsertCommand(DelfiFeedDataObject feed, SqlConnection connection)
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
