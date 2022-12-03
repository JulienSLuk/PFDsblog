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

        public int blogID { get; set; }

        [Required]
        public string blogName { get; set; }

        public string? blogImage { get; set; }

        //public string blogCat { get; set; }

        public IFormFile photoUpload { get; set; }

        public string blogDesc { get; set; }
        public string blogCat { get; set; }


    }
}
