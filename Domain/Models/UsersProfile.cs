﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UsersProfile 
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? VerificationToken { get; set; }
        public bool? IsVerified { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

    }
}
