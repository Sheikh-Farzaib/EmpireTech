using Application.Interfaces;
using Domain.Models;
using Infrastructure.AppSections;
using Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ConnectionString _connectionString;

        public UserProfileRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UsersProfile> UserLoginAsync(string email, string password)
        {
            string query = "SELECT * FROM UsersProfile WHERE IsDeleted = 0 AND IsVerified = 1 AND Email = @Email AND PasswordHash = @PasswordHash";

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@Email", email),
            new SqlParameter("@PasswordHash", password)
            };

            DataTable result = await ExecuteQueryNonQuery.ExecuteQueryAsync(query, parameters, _connectionString.GlobalConnection);

            if (result.Rows.Count > 0)
            {
                var userProfile = DataTableMapper.DataRowToObject<UsersProfile>(result.Rows[0]);

                return userProfile;
            }

            return null;
        }
        public async Task<bool> AddUserProfileAsync(UsersProfile userProfile)
        {
            string query = @"INSERT INTO UsersProfile (Id, UserName, Email, PasswordHash, VerificationToken, IsVerified, IsDeleted) 
                         VALUES (@Id, @UserName,@Email, @PasswordHash, @VerificationToken, @IsVerified, @IsDeleted)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Id", userProfile.Id),
            new SqlParameter("@UserName", userProfile.UserName),
            new SqlParameter("@Email", userProfile.Email),
            new SqlParameter("@PasswordHash", userProfile.PasswordHash),
            new SqlParameter("@VerificationToken", (object)userProfile.VerificationToken ?? DBNull.Value),
            new SqlParameter("@IsVerified", userProfile.IsVerified),
            new SqlParameter("@IsDeleted", userProfile.IsDeleted)
            };

           return await ExecuteQueryNonQuery.ExecuteNonQueryAsync(query, parameters, _connectionString.GlobalConnection);
           
        }
        public async Task<UsersProfile> GetUserProfileByIdAsync(Guid id)
        {
            string query = "SELECT * FROM UsersProfile WHERE Id = @Id AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };

            DataTable result = await ExecuteQueryNonQuery.ExecuteQueryAsync(query, parameters, _connectionString.GlobalConnection);

            if (result.Rows.Count > 0)
            {
                var userProfile = DataTableMapper.DataRowToObject<UsersProfile>(result.Rows[0]);

                return userProfile;
            }

            return null;
        } 
        public async Task<List<UsersProfile>> GetAllUserProfileAsync()
        {
            try
            {
                string query = "SELECT * FROM UsersProfile WHERE IsDeleted = 0";

                DataTable result = await ExecuteQueryNonQuery.ExecuteQueryAsync(query, _connectionString.GlobalConnection);

                if (result.Rows.Count > 0)
                {
                    var userProfile = DataTableMapper.DataTableToList<UsersProfile>(result);

                    return userProfile;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            
            return null;
        }
        public async Task<bool> UpdateUserProfileAsync(UsersProfile userProfile)
        {
            string query = @"UPDATE UsersProfile 
                         SET UserName = @UserName, Email = @Email, IsVerified = @IsVerified WHERE Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Id", userProfile.Id),
            new SqlParameter("@UserName", userProfile.UserName),
            new SqlParameter("@Email", userProfile.Email),
            new SqlParameter("@IsVerified", userProfile.IsVerified),
            };

            return await ExecuteQueryNonQuery.ExecuteNonQueryAsync(query, parameters, _connectionString.GlobalConnection);
        }
        public async Task<bool> DeleteUserProfileAsync(Guid id)
        {
            string query = "UPDATE UsersProfile SET IsDeleted = 1 WHERE Id = @Id AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Id", id)
            };

           return await ExecuteQueryNonQuery.ExecuteNonQueryAsync(query, parameters, _connectionString.GlobalConnection);
        }
        public async Task<UsersProfile> GetUserProfileByEmailAsync(string email)
        {
            string query = "SELECT * FROM UsersProfile WHERE Email = @Email AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Email", email)
            };

            DataTable result = await ExecuteQueryNonQuery.ExecuteQueryAsync(query, parameters, _connectionString.GlobalConnection);

            if (result.Rows.Count > 0)
            {
                var userProfile = DataTableMapper.DataRowToObject<UsersProfile>(result.Rows[0]);

                return userProfile;
            }
            return null;
        }
        public async Task<bool> VerifyTokenAsync(Guid token, string email)
        {
            string emailString = "";
            SqlParameter[] parameters = new SqlParameter[]
          {
            new SqlParameter("@VerificationToken", token),
          };
            if (!string.IsNullOrEmpty(email))
            {
                SqlParameter[] updatedParameters = new SqlParameter[parameters.Length + 1];

                Array.Copy(parameters, updatedParameters, parameters.Length);

                updatedParameters[parameters.Length] = new SqlParameter("@Email", email);

                parameters = updatedParameters;

                emailString = "AND Email = @Email";
            }

            string query = $"SELECT * FROM UsersProfile WHERE VerificationToken = @VerificationToken {emailString} AND IsDeleted = 0";

            DataTable result = await ExecuteQueryNonQuery.ExecuteQueryAsync(query, parameters, _connectionString.GlobalConnection);

            if (result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateUserEmailValidationFlagAsync(Guid Id)
        {
            string query = @"UPDATE UsersProfile 
                         SET IsVerified = 1 WHERE VerificationToken = @VerificationToken AND IsDeleted = 0";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@VerificationToken", Id),
            };

            return await ExecuteQueryNonQuery.ExecuteNonQueryAsync(query, parameters, _connectionString.GlobalConnection);
        }

    }
}
