﻿using System.ComponentModel.DataAnnotations;

namespace JofMVC1.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(65)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(65)]
        public string Surname { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(65)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
