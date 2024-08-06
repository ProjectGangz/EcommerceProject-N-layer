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
    public class ProductRepository : Repository<Product>, IProductRespository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
            //Work same
            //var objFromDB = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            //if (objFromDB != null)
            //{
            //    objFromDB.Title = obj.Title;
            //    objFromDB.Description = obj.Description;
            //    objFromDB.Price = obj.Price;
            //    objFromDB.CategoryId = obj.CategoryId;
            //    objFromDB.ListPrice = obj.ListPrice;
            //    objFromDB.ListPrice50 = obj.ListPrice50;
            //    objFromDB.ListPrice100 = obj.ListPrice100;
            //    if(obj.ImageUrl != null)
            //    {
            //        objFromDB.ImageUrl = obj.ImageUrl;
            //    }

            //}

        }
    }
}
