using ProjectEcommerce.DataAccess.Data;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRespository Category { get; private set; }
        public IProductRespository Product { get; private set; }
        public ICompanyRespository Company { get; private set; }
        public IShoppingCartRespository ShoppingCart { get; private set; }
        public IApplicationUserRespository ApplicationUser { get; private set; }
        public IOrderDetailRespository OrderDetail { get; private set; }

        public IOrderHeaderRespository OrderHeader { get; private set; }

        

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);          
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);

        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
