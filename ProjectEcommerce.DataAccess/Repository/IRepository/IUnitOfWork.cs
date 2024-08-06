using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        ICategoryRespository Category { get; }
        IProductRespository Product { get; }
        ICompanyRespository Company { get; }
        IShoppingCartRespository ShoppingCart { get; }
        IApplicationUserRespository ApplicationUser { get; }
        IOrderDetailRespository OrderDetail { get; }
        IOrderHeaderRespository OrderHeader { get; }
        void Save();
    }
}
