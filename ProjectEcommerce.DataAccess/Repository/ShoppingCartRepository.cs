using ProjectEcommerce.DataAccess.Data;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRespository
    {
        private ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       
        public void Update(ShoppingCart obj)
        {
            //_db.Categories.Update(obj);
            _db.ShoppingCarts.Update(obj);
        }
    }
}
