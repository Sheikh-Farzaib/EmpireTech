using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RegistrationDTO
    {
        public Guid? Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "A valid email is required.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>@]).+$", ErrorMessage = "Password must contain at least one special character.")]

        public string Password { get; set; }

    }
}
