using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
        public Summary Summary { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
