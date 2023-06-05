using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(AppDbContext context,
                              IProductService productService,
                              ICategoryService categoryService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }
      
        public async Task<IActionResult> Index()
        {
            List<Slide> sliders = await _context.Slides.Where(s => !s.IsDeleted).ToListAsync();
            IEnumerable<Category> categories = await _categoryService.GetAll();
            Summary summary = await _context.Summary.FirstOrDefaultAsync();
            IEnumerable<Product> products = await _productService.GetAll();


            HomeViewModel model = new HomeViewModel()
            {

                Slides = sliders,
                Summary = summary,
                Categories = categories,
                Products = products
            };
           

            return View(model);
        }
      

    }
}
