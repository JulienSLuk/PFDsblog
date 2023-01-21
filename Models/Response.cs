using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class Response
    {
        [Display(Name ="ResponseID")]
        public int ResponseID { get; set; }
        [Display(Name = "FeedbackID")]
        public int FeedbackID { get; set; }
        [Display(Name = "MemberID")]
        public string? MemberID { get; set; }
        [Display(Name = "StaffID")]
        public string? StaffID { get; set; }
        [Required (ErrorMessage = "Date time posted must be filled!")]
        [DataType(DataType.Date)]
        public DateTime DateTimePosted { get; set; }
        
        
        [Display(Name ="Response")]
        [Required(ErrorMessage = "Response must be filled")]
        public string Text { get; set; }
    }
}
