using UserActivity.DataAccess.Repository.IRepository;
using UserActivity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using UserActivity.Utility;
using UserActivity.Models.ViewModels;
using System.Linq;
using System;

namespace UserActivity.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole<int>> roleManager) {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult RoleManagment(int userId) {

            RoleManagmentVM RoleVM = new RoleManagmentVM() {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties:"Status"),
                RoleList = _roleManager.Roles.Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Name
                }),
                StatusList = _unitOfWork.Status.GetAll().Select(i => new SelectListItem {
                    Text = i.StatusName,
                    Value = i.StatusId.ToString()
                }),
            };

            RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u=>u.Id==userId))
                                         .GetAwaiter().GetResult().FirstOrDefault();
            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM) {

            string oldRole  = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == roleManagmentVM.ApplicationUser.Id))
                                .GetAwaiter().GetResult().FirstOrDefault();

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagmentVM.ApplicationUser.Id);


            if (!(roleManagmentVM.ApplicationUser.Role == oldRole)) {
                //a role was updated
                if (roleManagmentVM.ApplicationUser.Role == SD.Role_Admin) {
                    applicationUser.StatusId = roleManagmentVM.ApplicationUser.StatusId;
                }
                if (oldRole == SD.Role_Admin) {
                    applicationUser.StatusId = null;
                }
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();

            }
            else {
                if(oldRole==SD.Role_Admin && applicationUser.StatusId != roleManagmentVM.ApplicationUser.StatusId) {
                    applicationUser.StatusId = roleManagmentVM.ApplicationUser.StatusId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }

            return RedirectToAction("Index");
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Status").ToList();

            foreach(var user in objUserList) {
                user.Role=  _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                if (user.Status == null) {
                    user.Status = new Status() {
                        StatusName = ""
                    };
                }
            }

            return Json(new { data = objUserList });
        }


        [HttpPost]
        public IActionResult LockUnlock([FromBody]int id)
        {

            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null) 
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now) {
                //user is currently locked and we need to unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
