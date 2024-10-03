using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Reflection;

namespace Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserProfileRepository _user;
        private readonly IEmailNotificationRepository _emailNotification;

        public AuthController(IUserProfileRepository user, IEmailNotificationRepository emailNotification)
        {
            _user = user;
            _emailNotification = emailNotification;
        }
        public IActionResult Login() => View();
        public IActionResult Registration() => View();
        public IActionResult Verification() => View();        
        public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                        RegistrationDTO = loginDTO
                    };

                    return BadRequest(response);
                }
                var verificationToken = Guid.NewGuid().ToString();

                var userProfile = await _user.UserLoginAsync(loginDTO.Email, loginDTO.Password);

                if (userProfile != null)
                {
                    HttpContext.Session.SetString("UserId", userProfile.Id.ToString());
                    HttpContext.Session.SetString("Email", userProfile.Email);
                   await HttpContext.Session.CommitAsync();
                    return Ok(new { returnUrl = "/Users/User" });
                }
                    return BadRequest("Provided credential is not valid or you're not verified");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error Occurred");
            }
        }
        public async Task<IActionResult> UserRegistration([FromBody] RegistrationDTO registrationDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                        RegistrationDTO = registrationDTO
                    };

                    return BadRequest(response);
                }
                var verificationToken = Guid.NewGuid().ToString();

                //I hate manual mapping i had other things to so i decided to do it 
                bool isCreated = await _user.AddUserProfileAsync(new UsersProfile
                {
                    Id = registrationDTO.Id,
                    Email = registrationDTO.Email,
                    PasswordHash = registrationDTO.Password,
                    UserName = registrationDTO.UserName,
                    VerificationToken = verificationToken

                });

                if (isCreated)
                {
                    TempData["Email"] = registrationDTO.Email;
                    HttpContext.Session.SetString("Email", registrationDTO.Email);
                    await _emailNotification.SendEmailAsync(registrationDTO.Email, "Verification", verificationToken);
                    return Ok(new {returnUrl = "/auth/verification"});
                }
                return BadRequest("Not Created");
            }
            catch (Exception ex)
            {
                if (ex is SmtpException)
                {
                    return StatusCode(200, "User created successfully but email wasn't sent");
                }
                return StatusCode(500, "Error Occurred");
            }
        }
        public async Task<IActionResult> UserVerification(Guid token)
        {
            try
            {
                if (token == Guid.Empty || string.IsNullOrEmpty(token.ToString()))
                {
                    return BadRequest("Invalid token.");
                }

                bool isTokenValid = await _user.VerifyTokenAsync(token, HttpContext.Session.GetString("Email"));
                if (isTokenValid)
                {
                    bool isVerified = await _user.UpdateUserEmailValidationFlagAsync(token);
                    if (isVerified)
                        return Ok(new { returnUrl = "Login" });
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
