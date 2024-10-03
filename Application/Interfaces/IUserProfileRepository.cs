using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UsersProfile> UserLoginAsync(string email, string password);
        Task<bool> AddUserProfileAsync(UsersProfile userProfile);
        Task<UsersProfile> GetUserProfileByIdAsync(Guid id);
        Task<List<UsersProfile>> GetAllUserProfileAsync();
        Task<bool> UpdateUserProfileAsync(UsersProfile userProfile);
        Task<bool> DeleteUserProfileAsync(Guid id);
        Task<UsersProfile> GetUserProfileByEmailAsync(string email);
        Task<bool> VerifyTokenAsync(Guid token, string email);
        Task<bool> UpdateUserEmailValidationFlagAsync(Guid Id);
    }
}
