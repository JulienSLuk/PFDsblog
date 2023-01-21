using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class FeedbackViewModel
    {
        public int FeedbackID { get; set; }
        public string MemberID { get; set; }
        
        public DateTime DateTimePosted { get; set; }
      
        public string Title { get; set; }
        public string Text { get; set; }
        public string? ImageFileName { get; set; }
        public List<Response>ResponseList {get; set;}
    }
}
