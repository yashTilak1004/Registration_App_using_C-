/*
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Registration_App.Entities
{
    [Index(nameof(Email),IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UA
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
*/
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registration_App.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UA
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [MaxLength(20, ErrorMessage = "Max Length is 20")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 5, ErrorMessage = "Max Length is 20 and Minimum 5 need to be entered")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; } // Store gender as a string

        // To store skills, you may consider a separate entity or a simple list
        public List<string> Skills { get; set; } = new List<string>(); // You might want to store this in a different way for EF Core

        [Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; } // Store role as a string
    }
}
