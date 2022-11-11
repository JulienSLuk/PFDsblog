using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022Apr_P01_T3.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter the password")]
        public string Password { get; set; }


    }
}
