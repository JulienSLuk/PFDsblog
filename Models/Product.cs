using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product ID is required!")]
        public int ID { get; set; }
        [Required(ErrorMessage ="Please enter a title for the product")]
        [StringLength(255,ErrorMessage ="Title cannot exceed 255 characters.")]
        public string Title { get; set; }
        [StringLength(255,ErrorMessage ="File name exceeds 255 characters.")]
        public string? Image { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public string Desc { get; set; }
        public string Cat { get; set; }
        [StringLength(255, ErrorMessage = "File name exceeds 255 characters.")]
        [Display(Name = "Obsolete Status")]
        [RegularExpression(@"[0]|[1]$",ErrorMessage = "Invalid status, Status is identified using binary.")]
        public string ObsoleteStatus { get; set; }
    }
}
