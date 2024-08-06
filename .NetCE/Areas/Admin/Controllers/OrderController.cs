using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;

namespace ProjectEcommerce.Areas.Admin.Controllers
{
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			return View();
		}
		#region API CALL
		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> objOrderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

			return Json(new { data = objOrderHeader });
		}
		
		#endregion
	}
}
