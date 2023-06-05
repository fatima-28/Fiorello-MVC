using System.Collections.Generic;
using WebApp.DAL;
using WebApp.Services.Interfaces;
using WebApp.ViewModels.Basket;
using WebApp.ViewModels;
using System.Linq;

namespace WebApp.Services.Class
{
    public class LayoutService:ILayoutService
    {
        private readonly AppDbContext _context;
      

        public LayoutService(AppDbContext context)
        {
            _context = context;
           
        }

        public LayoutVM GetDatas()
        {
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
           
            return new LayoutVM { Settings = settings };
        }

    }
}
