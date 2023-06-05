using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.AdminPanel.ViewModels;
using WebApp.Areas.AdminPanel.ViewModels.Products;
using WebApp.DAL;
using WebApp.Helpers;
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
        
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null) return BadRequest();
            
                Product product = await _context.Products.Where(m=>m.IsDeleted==false).FirstOrDefaultAsync(m=>m.Id==id);
            if (product == null) return RedirectToAction("NotFound", "Error");

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
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.categories= _context.Categories.Where(c=>c.IsDeleted==false).ToList();  
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(ProductCreateVM model)
        {var categories= _context.Categories.Where(c => c.IsDeleted == false).ToList();

            if (!ModelState.IsValid) 
            {

                ViewBag.categories = categories;
                return View();
            }
          

           
            bool IsExist = _context.Products.Include(p => p.Images).Where(w => !w.IsDeleted).Any(w => w.Title.ToLower() == model.Title.ToLower());
            if (IsExist)
            {
                ModelState.AddModelError("Title", $"{model.Title} is already exist!");
                ViewBag.categories = categories;
                return View();

            }
            List<ProductImage> images = new List<ProductImage>();
            Product product = new Product();
            foreach (var photo in model.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "Selected file was not image");
                    ViewBag.categories = categories;
                    return View();
                }

            }
            foreach (var photo in model.Photos)
            {
                ProductImage image = new ProductImage()
                {
                    Url =await photo.SaveFileAsync(_env.WebRootPath,"img")
                };
                images.Add(image);
            }
            product.Images = images;
            product.Title=model.Title;
            product.Price=model.Price;
            product.Count=model.Count;
            product.CategoryId = model.CategoryId;
            product.Images.FirstOrDefault().IsMain = true;


            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

           

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var product= await _context.Products.Where(p=>!p.IsDeleted).FirstOrDefaultAsync(m=>m.Id==id);
            if (product==null)
            {
                return RedirectToAction("NotFound" ,"Error");
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Product model)
        {
           
            if (id == null)
            {
                return BadRequest();
            }
            var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var categories = _context.Categories.Where(c => c.IsDeleted == false).ToList();


            var IsExist = _context.Products.Where(w => !w.IsDeleted && w.Title == model.Title).FirstOrDefault();
            if (IsExist!=null)
            {
                ModelState.AddModelError("Title", $"{model.Title} is already exist!");
                ViewBag.categories = categories;
                return View(product);

            }
            if (model.Title==null) {
                ModelState.AddModelError("Title", $"Fill This field");
                ViewBag.categories = categories;
                return View(product);
            }
            var productDb = await _context.Products.FindAsync(id);

            productDb.Title = model.Title;
            productDb.Price = model.Price;
            productDb.Count = model.Count;
            productDb.CategoryId = model.CategoryId;
         
            _context.Update(productDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
