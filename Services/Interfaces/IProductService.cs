using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetAllById(int id);
        Task<List<Product>> GetPaginatedDatas(int page, int take);
        Task<Product> GetById(int id);
        Task<int> GetCount();
    }
}
