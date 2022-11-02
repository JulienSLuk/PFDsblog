using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WEB2022Apr_P01_T3.Models
{
    public class ProductManager
    {        

        public int productID { get; set; }

        [Required]
        public string productName { get; set; }

        public string? productImage { get; set; }

        [Range(20,500, ErrorMessage = "Price must be between $20 and $500 (inclusive)")]
        [Required]
        public decimal productPrice { get; set; }

        [Required]
        public DateTime productEffectiveDate { get; set; }
        
        [Required]
        public string productCondition { get; set; }

        public IFormFile photoUpload { get; set; }

  
    }
}
