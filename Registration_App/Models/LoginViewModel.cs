using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Registration_App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username or email is required.")]
        [MaxLength(20,ErrorMessage = "Max 20 characters allowed")]
        [DisplayName("UserName or Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
