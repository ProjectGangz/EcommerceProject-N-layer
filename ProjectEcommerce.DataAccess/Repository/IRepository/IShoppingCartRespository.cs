using ProjectEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRespository:IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
      
    }
}
