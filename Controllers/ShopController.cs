using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Class;
using WebApp.Services.Interfaces;
using WebApp.ViewModels.Basket;

namespace WebApp.Controllers
{
    public class ShopController : Controller
    {
            private readonly AppDbContext _context;
            private readonly IProductService _prodServices;
        private readonly IHttpContextAccessor _accessor;

        public ShopController(AppDbContext context, IProductService prodServices, IHttpContextAccessor accessor)
            {
                _context = context;
            _accessor = accessor;
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
        [HttpPost]
       
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _prodServices.GetById(id);

            if (product is null) return NotFound();

            List<BasketVM> basket = GetBasketDatas();

            AddProduct(basket, product);

            return Ok();
        }

        private List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Session.GetString("basket") == null)
            {
                basket = new List<BasketVM>();
            }
            else
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));
               
            }

            return basket;
        }

        private void AddProduct(List<BasketVM> basket, Product product)
        {
            BasketVM exProd = basket.FirstOrDefault(m => m.Id == product.Id);

            if (exProd is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                exProd.Count++;
            }

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basket));
        }
    }
}
