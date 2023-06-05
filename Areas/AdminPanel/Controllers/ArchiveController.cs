using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ArchiveController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Product> _products;
        private readonly IProductService _productService;
        public ArchiveController(IProductService productService, AppDbContext context)
        {
            _context = context;
            _productService = productService;
            _products = _context.Products.Include(m => m.Images).Where(m => m.IsDeleted).Include(m => m.Category).ToList();


        }
        public IActionResult GetArchivedProduct()
        {
            return View(_products);
        }
        [HttpPost]
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.Where(m => m.IsDeleted == true).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();


            product.IsDeleted = false;
            await _context.SaveChangesAsync();

            return Ok();



        }
    }
}
