using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {

        private AppDbContext _context { get; }
        private IEnumerable<Product> _products;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;


        public ProductController(IProductService productService,AppDbContext context, ICategoryService categoryService, IWebHostEnvironment env)
        {
           
            _env = env;
            _context = context;
            _productService = productService;
            _products = _context.Products.Include(m => m.Images).Where(m =>!m.IsDeleted).Include(m=>m.Category).ToList();
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_products);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{

        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null) return BadRequest();
            
                Product product = await _context.Products.Where(m=>m.IsDeleted==false).FirstOrDefaultAsync(m=>m.Id==id);
            if (product == null) return NotFound();

            _context.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
            
           

        }
        [HttpPost]
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.Where(m => m.IsDeleted == false).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();


            product.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok();



        }

    }
}
