using System.Collections.Generic;
using WebApp.DAL;
using WebApp.ViewModels;
using WebApp.ViewModels.Basket;

namespace WebApp.Services.Interfaces
{
    public interface ILayoutService
    {
        public LayoutVM GetDatas();
        
    }
}
