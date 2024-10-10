/*
using System.ComponentModel.DataAnnotations;



namespace Registration_App.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string FirstName { get; set; }
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage ="Please enter a valid Email.")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage ="Please add a valid email.")]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Registration_App.Models
{
    public class RegistrationViewModel
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
        [MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [MaxLength(20, ErrorMessage = "Max Length is 20")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max Length is 20 and Minimum 5 need to be entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please recheck the correct password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        // New fields
        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; } // For radio type
        // Option for gender could be an enum or string, but you might want to limit options

        //public List<string> GenderOptions { get; } = new List<string> { "Male", "Female", "Other" };
        [Required(ErrorMessage = "At least one skill must be selected")]
        public List<string> Skills { get; set; } = new List<string>(); // For checkbox type
        // Sample skill options (this could be replaced with actual options as needed)

        //public List<string> SkillOptions { get; set; } = new List<string> { "C#", "Java", "Python", "JavaScript", "HTML/CSS" };
        //[Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; } // For radio type
        // Role options (this could also be replaced with actual options)

        public List<string> RoleOptions { get; set; } = new List<string> { "User", "Admin", "Moderator" };
    }
}
/*

public class RegistrationViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public List<string> Skills { get; set; } = new List<string>(); // Adjusted for List<string>
    public string Role { get; set; }
}
 

 */






