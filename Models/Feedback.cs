using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public string MemberID { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTimePosted { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage =
            "Title must be filled")]
        public string Title { get; set; }
        [Display(Name="Feedback")]
        public  string Text { get; set; }
        public string? ImageFileName  { get; set; }


    }
}
