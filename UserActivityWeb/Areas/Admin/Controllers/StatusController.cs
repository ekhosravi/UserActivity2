using UserActivity.DataAccess.Repository.IRepository;
using UserActivity.Models;
using UserActivity.Models.ViewModels;
using UserActivity.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace UserActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class StatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index() 
        {
            List<Status> objStatusList = _unitOfWork.Status.GetAll().ToList();
           
            return View(objStatusList);
        }

        public IActionResult Upsert(int? id)
        {
           
            if (id == null || id == 0)
            {
                //create
                return View(new Status());
            }
            else
            {
                //update
                Status statusObj = _unitOfWork.Status.Get(u => u.StatusId == id);
                return View(statusObj);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Status StatusObj)
        {
            if (ModelState.IsValid)
            {
                
                if (StatusObj.StatusId == 0)
                {
                    _unitOfWork.Status.Add(StatusObj);
                }
                else
                {
                    _unitOfWork.Status.Update(StatusObj);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Status created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(StatusObj);
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Status> objStatusList = _unitOfWork.Status.GetAll().ToList();
            return Json(new { data = objStatusList });
        }


        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{
        //    var StatusToBeDeleted = _unitOfWork.Status.Get(u => u.StatusId == id);
        //    if (StatusToBeDeleted == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }

        //    _unitOfWork.Status.Remove(StatusToBeDeleted);
        //    _unitOfWork.Save();

        //    return Json(new { success = true, message = "Delete Successful" });
        //}

        #endregion
    }
}
