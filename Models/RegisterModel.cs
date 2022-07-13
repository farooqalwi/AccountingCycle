using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Password", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
