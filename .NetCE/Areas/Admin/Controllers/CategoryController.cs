using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.DataAccess.Data;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;
using ProjectEcommerce.Utility;


namespace ProjectEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<CategoryModel> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(CategoryModel oject)
        {
            //if(oject.CategoryName == oject.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CategoryName", "Name canot equal Oder");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(oject);
                //CategoryModel model = new CategoryModel()
                //{
                //    CategoryId = oject.CategoryId,
                //    CategoryDescription = oject.CategoryDescription,
                //    CategoryName = oject.CategoryName,
                //    DisplayOrder = oject.DisplayOrder,
                //};
                _unitOfWork.Save();
                TempData["Success"] = "Create successful";
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //1way
            //CategoryModel? model = _db.Categories.Find(id);
            //2way
            CategoryModel? model = _unitOfWork.Category.Get(e => e.CategoryId == id);
            //3way
            //CategoryModel model2 = _db.Categories.Where(e=>e.CategoryId == id).FirstOrDefault();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CategoryModel category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();

                TempData["Success"] = "Update successful";
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            CategoryModel model = _unitOfWork.Category.Get(e => e.CategoryId == id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            CategoryModel ojectDetele = _unitOfWork.Category.Get(e => e.CategoryId == id);
            if (ojectDetele == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(ojectDetele);
            _unitOfWork.Save();
            TempData["Success"] = "Delete successful";
            return RedirectToAction("Index");
        }
    }
}
