using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services.Class
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var res= await _context.Products.Include(m => m.Images).Where(m=>!m.IsDeleted).ToListAsync();
            return res;
        }

      

        public async Task<Product> GetById(int? id)
        {
           var res= await _context.Products.FindAsync(id);
            return res;
        }

        public async Task<int> GetCount()
        {
            var res= await _context.Products.CountAsync();
            return res;
        }

        public async Task<List<Product>> GetPaginatedDatas(int page, int take)
        {
            var res = await _context.Products.Include(m => m.Category).Include(m => m.Images).Skip((page * take) - take).Take(take).ToListAsync(); ;
            return res;
        }
    }
}
