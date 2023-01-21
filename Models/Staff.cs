using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class Staff
    {
        [Display(Name = "ID")]
        [StringLength(20, ErrorMessage = "Invalid ID Length")]
        public string StaffID { get; set; }
        [Display(Name = "Assigned Store")]
        [StringLength(25, ErrorMessage = "The Branch ID is too long.")]
        public string? StoreID { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gender must be filled")]
        [Display(Name = "Gender*")]
        [RegularExpression(@"[M]|[F]$", ErrorMessage = "Invalid gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please fill in the staff appointment")]
        [StringLength(50,ErrorMessage = "Position Name cannot exceed 50 Characters.")]
        public string Appointment { get; set; }
        [StringLength(8,ErrorMessage ="Contact number cannot exceed 8 digits")]
        [RegularExpression("6#######",ErrorMessage = "Staff contacts begin with '6'.")]
        public int TelNo { get; set; }
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20,ErrorMessage ="Password cannot exceed 20 characters")]
        public  string Password { get; set; }
    }
}
