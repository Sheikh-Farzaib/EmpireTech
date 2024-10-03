using Application.DTO;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserProfileRepository _user;

        public UsersController(IUserProfileRepository user)
        {
            _user = user;
        }
        public IActionResult User() => View();

        public async Task<IActionResult> UserData()
        {
            try
            {
                var userData = await _user.GetAllUserProfileAsync();
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest("Something Went Wrong");
            }
        }

        public async Task<IActionResult> UserDelation(Guid id)
        {
            try
            {
                if (id == Guid.Empty || string.IsNullOrEmpty(id.ToString()))
                {
                    return BadRequest("Invalid Id.");
                }

                bool isDeleted = await _user.DeleteUserProfileAsync(id);
                if (isDeleted)
                {
                      return Ok(new { result = "Deleted Successfully" });
                }
                return BadRequest("Not Created");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error Occurred");
            }
        }
        public async Task<IActionResult> UserModification([FromBody] UpdateUserDTO updateUser)
        {
            try
            {
                if (updateUser?.Id == Guid.Empty || string.IsNullOrEmpty(updateUser?.Id.ToString()))
                {
                    return BadRequest("Invalid Id.");
                }

                var isUserExist = await _user.GetUserProfileByIdAsync(updateUser.Id.Value);
                if (isUserExist == null)
                    return StatusCode(200, "Not Found");
                bool isUpdated = await _user.UpdateUserProfileAsync(new UsersProfile
                {
                    Id = updateUser.Id,
                    Email = updateUser.Email ?? isUserExist.Email,
                    IsVerified = updateUser.IsVerified ?? isUserExist.IsVerified,
                    UserName = updateUser.UserName ?? isUserExist.UserName,
                });
                if (isUpdated)
                {
                    return Ok(new { result = "Updated Successfully" });
                }
                return BadRequest("Not Created");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error Occurred");
            }
        }

    }
}
