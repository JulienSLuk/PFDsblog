using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022Apr_P01_T3.Models
{
    public class FeedbackViewModel
    {
        public int FeedbackID { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        public DateTime DateTimePosted { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(255, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter some feedback")]
        public string? Text { get; set; }

        [StringLength(255, ErrorMessage = "File name is too long")]
        public string? ImageFileName { get; set; }

        public IFormFile? fileToUpload { get; set; }

        public List<Response> ResponseList { get; set; } = new List<Response>();
    }
}
