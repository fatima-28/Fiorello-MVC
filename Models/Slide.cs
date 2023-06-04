using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Slide
    {
       
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Url { get; set; }
        [NotMapped,Required]
        public IFormFile Photo { get; set; }
    }
}
