using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProjectEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category"),
                Count = 1,
                ProductId = id

            };
        
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shopping)     
        {
            shopping.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shopping.UserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.UserId == userId && u.ProductId == shopping.ProductId);

            

            if (cartFromDb != null)
            {
                //Shopping cart is exit
                cartFromDb.Count = shopping.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                //Shopping cart record
                _unitOfWork.ShoppingCart.Add(shopping);
            }
            TempData["success"] = "Added Item to cart";

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
