using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.AdminPanel.ViewModels.Products
{
    public class ProductEditVM
    {
       
        [Required(ErrorMessage = "This fields is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This fields is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "This fields is required")]
        public int Count { get; set; }

        [Required(ErrorMessage = "This fields is required")]

        public int CategoryId { get; set; }
    }
}
