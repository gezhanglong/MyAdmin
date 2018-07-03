using System.Linq;
using System.Web.Mvc;
using MyProject.Matrix.Controllers.SysManage.ViewModels;
using MyProject.Core.Entities;
using MyProject.Tasks;
using MyProject.Services.Mappers;
using MyProject.Task;
using System;
using MyProject.Matrix.Controllers.Home.ViewModels;
using MyProject.Services.Utility;
using MyProject.Core.Dtos;
using MyProject.Core.Enum;
using System.Web;

namespace MyProject.Matrix.Controllers.Home
{
    public class HomeController : BaseController
    { 
        private readonly AdminRoleMenuTask _roleMenuTask = new AdminRoleMenuTask();
        private readonly AdminMenuTask _menusTask = new AdminMenuTask();
        private readonly AdminUserTask _adminUserTask = new AdminUserTask();

        #region 主页面
        public ActionResult Index()
        {
            if (GetCurrentAdmin() == null)
            {
                return Redirect("/Home/LogInIndex");
            }
            ViewBag.account = GetCurrentAdmin();
            var userPassword = _adminUserTask.GetByUserName(GetCurrentAdmin());
            var menuIds = _roleMenuTask.GetListByRoleId(userPassword.RoleId)
                .Select(c => c.MenuId).ToList();
            return View();
        }

        public ActionResult Menu()
        {
            var userPassword = _adminUserTask.GetByUserName(GetCurrentAdmin());
            var menus = _roleMenuTask.GetMenuLstByRoleId(userPassword.RoleId)
                .Select(EntityMapper.Map<AdminMenu, MenuModel>)
                .OrderBy(c => c.SortOrder)
                .ToList();
            return View(menus);
        }

        public ActionResult FirstIndex()
        {
            return View();
        } 
        #endregion 

        #region 登录 
        public ActionResult LogInIndex()
        {
            var model = new LogOnModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LogInIndex(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                var userPassword = _adminUserTask.GetByUserName(model.UserName);
                if (userPassword == null)
                {
                    ModelState.AddModelError("UserName", "账号不存在");
                    return View(model);
                } 
                if (userPassword.Password != CryptTools.HashPassword(model.Password))
                {
                    ModelState.AddModelError("UserName", "密码不正确");  
                    return View(model);
                }  
                if (userPassword.IsLock) 
                {
                    ModelState.AddModelError("Password", "对不起，您的账号被锁定");
                    return View(model);
                } 

                HttpCookie cookie = new HttpCookie("Account", userPassword.UserName); 
                cookie.Expires = DateTime.Now.AddMinutes(60);  
                Response.Cookies.Add(cookie);

                SysLogTask.AddLog(new MyProject.Core.Entities.SysLogDto() { Message = "", Module = LogModuleEnum.Land, Type = LogTypeEnum.Land, Operator = userPassword.UserName, Result = "登陆成功" });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        #endregion

        #region 退出
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Account", ""); 
            cookie.Expires = DateTime.Now.AddDays(-1); 
            Response.Cookies.Add(cookie); 
            return RedirectToAction("LogInIndex", "Home");
        }
        #endregion

    

    }
}