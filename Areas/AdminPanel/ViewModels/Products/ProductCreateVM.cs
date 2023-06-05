using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Areas.AdminPanel.ViewModels
{
    public class ProductCreateVM
    {
        
        [Required(ErrorMessage = "This fields is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This fields is required")]
        public double Price { get; set; } 

        [NotMapped, Required(ErrorMessage = "This fields is required")]
        public List<IFormFile> Photos { get; set; }

        [Required(ErrorMessage = "This fields is required")]
        public int Count { get; set; } 

        [Required(ErrorMessage = "This fields is required")]

        public int CategoryId { get; set; }
    }
}
