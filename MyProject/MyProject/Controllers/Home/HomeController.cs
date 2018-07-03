using System.Linq;
using System.Web.Mvc;
using MyProject.Controllers.SysManage.ViewModels;
using MyProject.Core.Entities;
using MyProject.Tasks;
using MyProject.Services.Mappers;
using MyProject.Task;
using System;

namespace MyProject.Controllers.Home
{
    public class HomeController : BaseController
    { 
        private readonly AdminRoleMenuTask _roleMenuTask = new AdminRoleMenuTask();
        private readonly AdminMenuTask _menusTask = new AdminMenuTask();

        public ActionResult Index()
        {
            if (GetCurrentAdmin() == null)
            {
                return Redirect("/account/index");
            }
            ViewBag.account = GetCurrentAdmin().UserName;
            var menuIds = _roleMenuTask.GetListByRoleId(GetCurrentAdmin().RoleId)
                .Select(c => c.MenuId).ToList();
            return View();
        }

        public ActionResult Menu()
        {
            var menus = _roleMenuTask.GetMenuLstByRoleId(GetCurrentAdmin().RoleId)
                .Select(EntityMapper.Map<AdminMenu, MenuModel>)
                .OrderBy(c => c.SortOrder)
                .ToList();
            return View(menus);
        }

        public ActionResult FirstIndex()
        { 
            return View();
        }
    }
}