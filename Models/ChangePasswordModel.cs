using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingCycle.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Email is Required.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Old Password is Required.")]
        [Display(Name = "Old Password: ")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New Password: ")]
        [Required(ErrorMessage = "New Password is Required.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password: ")]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Password does not match.")]
        public string ConfirmNewPassword { get; set; }

    }
}
