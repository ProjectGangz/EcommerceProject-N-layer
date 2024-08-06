using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectEcommerce.DataAccess.Data;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;
using ProjectEcommerce.Models.ViewModels;


namespace ProjectEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objProductList);
        }
        [HttpGet]
        //Update and Create
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.CategoryId.ToString()
            });
			//ViewBag.CategoryList = CategoryList;
			//ViewData["CategoryItems"] = CategoryList;
            ProductVM productVM = new ProductVM()
            {
                CategotyList = CategoryList,
                Product = new Product()
            };
            if(id==null ||  id == 0)
            {
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u =>u.Id == id);
                return View(productVM);
            }
			
        }

        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {         
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"/images/product/"+fileName;
                }
                if(productVM.Product.Id == 0)
                {
					_unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
              
                _unitOfWork.Save();
                TempData["Success"] = "Create successful";
                return RedirectToAction("Index");
            }
            else
            {
				productVM.CategotyList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.CategoryName,
					Value = u.CategoryId.ToString()
				});
				return View(productVM);
			}
        }
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //1way
        //    //Product? model = _db.Categories.Find(id);
        //    //2way
        //    Product? productFromDb = _unitOfWork.Product.Get(e => e.Id == id);
        //    //3way
        //    //Product model2 = _db.Categories.Where(e=>e.CategoeyID == id).FirstOrDefault();
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product Product)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(Product);
        //        _unitOfWork.Save();

        //        TempData["Success"] = "Update successful";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product productFromDb = _unitOfWork.Product.Get(e => e.Id == id);

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product ojectDetele = _unitOfWork.Product.Get(e => e.Id == id);
        //    if (ojectDetele == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(ojectDetele);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Delete successful";
        //    return RedirectToAction("Index");
        //}
        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new {data = objProductList});
        }
        // add HttpDelete to use sweetalert2
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productDelete = _unitOfWork.Product.Get(u => u.Id == id);

            if(productDelete == null)
            {
                return Json(new {success = false, message="Error while delete"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productDelete.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productDelete);
            _unitOfWork.Save();
            return Json(new { success = false, message = "Delete success" });
        }
        #endregion
    }
}
