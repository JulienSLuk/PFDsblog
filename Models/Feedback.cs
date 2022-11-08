using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WEB2022Apr_P01_T3.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        
        [Display(Name = "Email")]
        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string Email { get; set; }

        public DateTime DateTimePosted { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(255, ErrorMessage = "Please enter a shorter title")]
        public string Title { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Please enter some feedback")]
        public string? Text { get; set; }

        [Display(Name = "Image")]
        [StringLength(255)]
        public string ImageFileName { get; set; }
    }
}
