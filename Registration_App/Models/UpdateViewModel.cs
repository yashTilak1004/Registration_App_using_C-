using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Registration_App.Models
{
    public class UpdateViewModel
    {
        //Id is untouched,it is there for fetching data
        //Id is untouched,it is there for fetching data
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }
}







