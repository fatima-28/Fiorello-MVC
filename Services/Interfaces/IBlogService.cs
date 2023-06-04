using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services.Classes
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAll();
    }
}
