using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public  class resetviemodel
    {
        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 1 lowercase,1 uppercase, 1 digit,1 special character and must be of 8 characters")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm Password is not match with Password")]
        public string ConfirmPassword { get; set; } = null!;

     
    }
}
