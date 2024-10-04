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
