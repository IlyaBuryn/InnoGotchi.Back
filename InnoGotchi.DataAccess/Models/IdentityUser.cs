﻿using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.DataAccess.Models
{
    public class IdentityUser : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Image { get; set; }
        public IdentityRole Role { get; set; }
    }
}
