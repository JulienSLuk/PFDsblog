using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022Apr_P01_T3.Models
{
    public class Response
    {
        public int ResponseID { get; set; }
        
        [Display(Name ="Feedback ID")]
        [Required(ErrorMessage = "Please enter Feedback ID")]
        public int FeedbackID { get; set; }

        [StringLength(9, MinimumLength = 9)]
        public string? MemberID { get; set; }

        [StringLength(20)]
        public string? StaffID { get; set; }

        public DateTime DateTimePosted { get; set; }

        [Display(Name = "Response")]
        [Required(ErrorMessage = "Please enter a message")]
        public string Text { get; set; }
    }
}
