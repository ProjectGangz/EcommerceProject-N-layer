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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRespository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       
        public void Update(OrderHeader obj)
        {
            //_db.Categories.Update(obj);
            _db.OrderHeaders.Update(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u =>u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrdersStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntenId)

		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u =>u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
			if (!string.IsNullOrEmpty(paymentIntenId))
			{
				orderFromDb.PaymentIntentId = paymentIntenId;
                orderFromDb.PaymentDate = DateTime.Now;
			}
		}
	}	
}
