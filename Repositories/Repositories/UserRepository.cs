using Microsoft.Extensions.Configuration;
using Repositories.Constants;
using Repositories.DataObject;
using Repositories.Interfaces;
using Shared.Constants;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    /// <summary>
    /// Repository class for User table
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private IConfiguration _config;
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets user by ID. If user is not found return null
        /// </summary>
        public async Task<UserDataObject> GetUserByID(string userID)
        {
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(ProcedureNameConstants.GetUserByID, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    var reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        return new UserDataObject
                        {
                            UserID = reader.GetString(reader.GetOrdinal(UserTableColumNameConstants.UserID)),
                            Email = reader.GetString(reader.GetOrdinal(UserTableColumNameConstants.Email)),
                            FullName = reader.GetString(reader.GetOrdinal(UserTableColumNameConstants.FullName))
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Create or updates user. Add profile picture in UsersImage table
        /// </summary>
        public async Task CreateOrUpdateUser(string userID, string email, string fullName, byte[] picture)
        {
            var connectionString = _config.GetSection(ConfigFileConstants.DatabaseConnectionString).Value;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(ProcedureNameConstants.CreateOrUpdateUser, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Picture", picture);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
