using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.ViewModels.Basket;

namespace WebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public BasketController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketFullInfoVM> basketList = new List<BasketFullInfoVM>();

            if (_accessor.HttpContext.Session.GetString("basket") != null)
            {
                List<BasketVM> basketDatas = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));

                foreach (var item in basketDatas)
                {
                    var product = await _context.Products.Include(m=>m.Images).Where(m=>!m.IsDeleted).FirstOrDefaultAsync(m => m.Id == item.Id);

                    if (product != null)
                    {
                        BasketFullInfoVM basketDetail = new BasketFullInfoVM()
                        {
                            Id = product.Id,
                            Name = product.Title,
                            Image = product.Images.FirstOrDefault().Url,
                            Count = item.Count,
                            Price = product.Price,
                            TotalPrice = item.Count * product.Price
                        };

                        basketList.Add(basketDetail);
                    }
                }
            }

            return View(basketList);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            var products = JsonSerializer.Deserialize<List<BasketFullInfoVM>>(_accessor.HttpContext.Session.GetString("basket"));

            var removingProduct = products.FirstOrDefault(m => m.Id == id);

            int deleteIndex = products.IndexOf(removingProduct);

            products.RemoveAt(deleteIndex);

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(products));

            return Ok();
        }
    }
}
