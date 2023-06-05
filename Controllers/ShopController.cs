using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class ShopController : Controller
    {
            private readonly AppDbContext _context;
            private readonly IProductService _prodServices;

            public ShopController(AppDbContext context, IProductService prodServices)
            {
                _context = context;
            _prodServices = prodServices;
            }

            public async Task<IActionResult> Index()
            {

                var count = await _context.Products.Where(m => !m.IsDeleted).CountAsync();

                ViewBag.Count = count;

                IEnumerable<Product> products = await _context.Products.Include(m => m.Images).Where(m => !m.IsDeleted).Take(4).ToListAsync();
                return View(products);
            }

            public async Task<IActionResult> LoadMore(int skip)
            {
            IEnumerable<Product> products = await _context.Products.Include(m => m.Images).Where(m => !m.IsDeleted).Skip(skip).Take(4).ToListAsync();


            return PartialView("_ProductPartial", products);
            }
        
    }
}
