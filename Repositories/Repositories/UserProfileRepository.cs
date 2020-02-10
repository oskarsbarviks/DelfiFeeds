using Microsoft.Extensions.Configuration;
using Repositories.Constants;
using Repositories.DataObject;
using Repositories.Interfaces;
using Shared.Constants;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private IConfiguration _config;
        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<UserProfileDataObject> GetUserProfileDataByUserID(string userID, string defaultCategory, int feedCount)
        {
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(ProcedureNameConstants.GetUserProfileData, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        using (var sqlStream = reader.GetStream(reader.GetOrdinal(UsersImageTableNAmeConstants.Picture)))
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            sqlStream.CopyTo(memoryStream);

                            var userProfile = new UserProfileDataObject
                            {
                                Category = reader.IsDBNull(reader.GetOrdinal(FeedsSettingsTableColumnConstants.Category)) ? defaultCategory : reader.GetString(reader.GetOrdinal(FeedsSettingsTableColumnConstants.Category)),
                                Email = reader.GetString(reader.GetOrdinal(UserTableColumNameConstants.Email)),
                                FullName = reader.GetString(reader.GetOrdinal(UserTableColumNameConstants.FullName)),
                                FeedsCount = reader.IsDBNull(reader.GetOrdinal(FeedsSettingsTableColumnConstants.FeedCount)) ? feedCount : reader.GetInt32(reader.GetOrdinal(FeedsSettingsTableColumnConstants.FeedCount)),
                                Picture = memoryStream.ToArray()
                            };

                            await reader.CloseAsync();
                            return userProfile;
                        }
                    }
                    else
                    {
                        await reader.CloseAsync();
                        // For production I would create our own custom exception
                        throw new ArgumentException($"User defined by userID: {userID} is not found");
                    }
                }
            }
        }
    }
}
