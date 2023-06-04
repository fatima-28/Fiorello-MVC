using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services.Class
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
           var res=await _context.Categories.ToListAsync();
            return res;
        }

        public async Task<Category> GetById(int id)
        {
            var res = await _context.Categories.FindAsync(id);
            return res;
        }
    }
}
