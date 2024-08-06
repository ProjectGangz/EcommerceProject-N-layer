using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectEcommerce.DataAccess.Data;
using ProjectEcommerce.DataAccess.Repository.IRepository;
using ProjectEcommerce.Models;
using ProjectEcommerce.Models.ViewModels;


namespace ProjectEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
       
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }
        [HttpGet]
        //Update and Create
        public IActionResult Upsert(int? id)
        {
            
            if(id==null ||  id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //update
                Company  company= _unitOfWork.Company.Get(u =>u.Id == id);
                return View(company);
            }
			
        }

        public IActionResult Upsert(Company companyObj)
        {         
            if (ModelState.IsValid)
            {
                
               
                if(companyObj.Id == 0)
                {
					_unitOfWork.Company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }
                
              
                _unitOfWork.Save();
                TempData["Success"] = "Create successful";
                return RedirectToAction("Index");
            }
            else
            {				
				return View(companyObj);
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
        //    //Company? model = _db.Categories.Find(id);
        //    //2way
        //    Company? productFromDb = _unitOfWork.Company.Get(e => e.Id == id);
        //    //3way
        //    //Company model2 = _db.Categories.Where(e=>e.CategoeyID == id).FirstOrDefault();
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company Company)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(Company);
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

        //    Company productFromDb = _unitOfWork.Company.Get(e => e.Id == id);

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Company ojectDetele = _unitOfWork.Company.Get(e => e.Id == id);
        //    if (ojectDetele == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(ojectDetele);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Delete successful";
        //    return RedirectToAction("Index");
        //}
        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new {data = objCompanyList});
        }
        // add HttpDelete to use sweetalert2
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productDelete = _unitOfWork.Company.Get(u => u.Id == id);

            if(productDelete == null)
            {
                return Json(new {success = false, message="Error while delete"});
            }
            
            _unitOfWork.Company.Remove(productDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete success" });
        }
        #endregion
    }
}
