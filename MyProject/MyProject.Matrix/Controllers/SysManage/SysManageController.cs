using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyProject.Matrix.Controllers.SysManage.ViewModels;
using MyProject.Core.Entities;
using MyProject.Tasks;
using MyProject.Services.Extensions;
using MyProject.Services.Mappers;
using MyProject.Services.MvcPager;
using MyProject.Matrix.Controllers.Core;
using Newtonsoft.Json;
using MyProject.Core.Dtos;

namespace MyProject.Matrix.Controllers.SysManage
{
    public class SysManageController : BaseController
    {
        private readonly AdminMenuTask _menusTask = new AdminMenuTask();
        private readonly AdminPowerTask _powersTask = new AdminPowerTask();
        private readonly AdminUserRoleTask _userRoleTask = new AdminUserRoleTask();
        private readonly AdminRoleMenuTask _roleMenuTask = new AdminRoleMenuTask();
        private readonly AdminRolePowerTask _rolePowerTask = new AdminRolePowerTask();
        private readonly AdminUserTask _adminUserTask = new AdminUserTask();

        #region 菜单管理
        [SupportFilter(ActionName = "MenuList")]
        public ActionResult MenuList()
        { 
            ViewBag.perm = GetPermission(); 
            var menuList = _menusTask.GetAll() ;
            var treeParentList = new List<MenuTreeNode>();
            foreach (var p in menuList.Where(c=>c.ParentId==0).ToList())
            {
                var treeParentNode = new MenuTreeNode();
                treeParentNode.text = p.MenuName +"_"+p.MenuId;
                var ParentState = new MenuTreeState() { checkedd=false ,disabled=false ,expanded=false ,selected=false };
                treeParentNode.state = ParentState;
                var treeChildList = new List<MenuTreeNode>();
                foreach (var c in menuList.Where(c => c.ParentId == p.MenuId).ToList())
                {
                    var treeChildNode = new MenuTreeNode();
                    treeChildNode.text = c.MenuName + "_" + c.MenuId;
                    var ChildState = new MenuTreeState() { checkedd=false ,disabled=false ,expanded=false ,selected=false };
                    treeChildNode.state = ChildState;
                    treeChildList.Add(treeChildNode);
                }
                treeParentNode.nodes = treeChildList;
                treeParentList.Add(treeParentNode);
            }
            ViewData["treeList"] = JsonConvert.SerializeObject(treeParentList).Replace("checkedd", "checked"); ;

            var parentList = _menusTask.GetParentList().ToSelectList(c => c.MenuId.ToString(), c => c.MenuName);
            parentList.Insert(0, new SelectListItem { Text = "根菜单", Value = "0" });
            ViewData["ParentList"] = parentList;

            return View();
        }

        //根据菜单ID获取菜单信息
        [HttpPost]
        public ActionResult GetMenuInfoByMenuId(int? menuId)
        {
            var info = _menusTask.GetByMenuId((int)menuId);
            var model = EntityMapper.Map<AdminMenu, SaveMenuModel>(info);
            return Json(model);
        }

    
        [SupportFilter(ActionName = "SaveMenu")]
        [HttpPost]
        public ActionResult SaveMenu(SaveMenuModel model)
        { 
                if (model.MenuId == 0)
                {
                    var info = new AdminMenu
                                   {
                                       CreateDate = DateTime.Now,
                                       LinkUrl = model.LinkUrl,
                                       MenuName = model.MenuName,
                                       ParentId = model.ParentId,
                                       SortOrder = model.SortOrder,
                                   };
                    _menusTask.Add(info);
                    return Json(new RequestResultDto() { Ret = 0, Msg = "添加成功" }); 
                }
                else
                {
                    var info = _menusTask.GetByMenuId((int)model.MenuId);
                    if (info != null)
                    {
                        info.LinkUrl = model.LinkUrl;
                        info.MenuName = model.MenuName;
                        info.ParentId = model.ParentId;
                        info.SortOrder = model.SortOrder;
                        _menusTask.Update(info);
                    } 
                    return Json(new RequestResultDto() { Ret = 0, Msg = "修改成功" }); 
                }
               
        }

        [SupportFilter(ActionName = "DeleteMenu")]
        [HttpPost]
        public void DeleteMenu(int? menuId)
        {
            _menusTask.DeleteByMenuId(Convert.ToInt32(menuId));
        }

        #endregion

        #region 权限管理
        [SupportFilter(ActionName = "PowerList")]
        public ActionResult GetPowerListByMenuId(int menuId)
        {
            var powerList = _powersTask.GetListByMenuId(menuId); 
            return Json(powerList);
        }

        [SupportFilter(ActionName = "SavePower")]
        [HttpPost]
        public ActionResult SavePower(SavePowerModel model)
        { 
                var info = new AdminPower
                               {
                                   Action = model.Action,
                                   Controller = model.Controller,
                                   CreateDate = DateTime.Now,
                                   MenuId = model.MenuId,
                                   PowerCode = model.PowerCode,
                                   PowerName = model.PowerName,
                               };
                _powersTask.Add(info); 
                return Json(new RequestResultDto() { Ret = 0, Msg = "添加成功" }); 
        }

        [SupportFilter(ActionName = "DeletePower")]
        [HttpPost]
        public void DeletePower(int powerId)
        {
            _powersTask.DeleteByPowerId(powerId);
        }

        #endregion

        #region 角色管理
        [SupportFilter(ActionName = "RoleList")]
        public ActionResult RoleList()
        {
            ViewBag.perm = GetPermission();
            var roleList = _userRoleTask.GetAll()
                .Select(EntityMapper.Map<AdminUserRole, UserRoleModel>)
                .ToList();
            return View(roleList);
        }
        [SupportFilter(ActionName = "SaveRole")]
        public ActionResult SaveRole(int? roleId)
        {
            var allMenus = _menusTask.GetAll();
            var allPowers = _powersTask.GetAll();
            var roleMenus = new List<AdminRoleMenu>();
            var rolePowers = new List<AdminRolePower>();

            var model = new SaveUserRoleModel();

            if (roleId != null)
            {
                var info = _userRoleTask.GetByRoleId((int)roleId);
                if (info != null)
                {
                    model = EntityMapper.Map<AdminUserRole, SaveUserRoleModel>(info);
                    roleMenus = _roleMenuTask.GetListByRoleId(info.RoleId);
                    rolePowers = _rolePowerTask.GetListByRoleId(info.RoleId);
                }
            }

            ViewData["PowerList"] = (from a in allPowers
                                     join b in rolePowers on a.PowerId equals b.PowerId into temp
                                     from c in temp.DefaultIfEmpty()
                                     select new RolePowerModel
                                                {
                                                    IsSelected = c != null,
                                                    MenuId = a.MenuId,
                                                    PowerId = a.PowerId,
                                                    PowerName = a.PowerName
                                                }).ToList();
            ViewData["MenuList"] = (from a in allMenus
                                    join b in roleMenus on a.MenuId equals b.MenuId into temp
                                    from c in temp.DefaultIfEmpty()
                                    select new RoleMenuModel
                                               {
                                                   IsSelected = c != null,
                                                   MenuId = a.MenuId,
                                                   MenuName = a.MenuName,
                                                   ParentId = a.ParentId
                                               }).ToList();
            return View(model);
        }
        [SupportFilter(ActionName = "SaveRole")]
        [HttpPost]
        public ActionResult SaveRole(SaveUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var powerIds = Request["PowerId"] == null
                                   ? new List<int>()
                                   : Request["PowerId"].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(c => Convert.ToInt32(c)).ToList();
                var menuIds = Request["MenuId"] == null
                    ? new List<int>()
                    : Request["MenuId"].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(c => Convert.ToInt32(c)).ToList();

                if (model.RoleId == null)
                    _userRoleTask.Create(model.RoleName, menuIds, powerIds);
                else
                    _userRoleTask.Update((int)model.RoleId, model.RoleName, menuIds, powerIds);
                return AlertMsg("保存成功", Request.UrlReferrer.PathAndQuery);
            }
            return AlertMsg("参数不正确", Request.UrlReferrer.PathAndQuery);
        }

        #endregion
         
    }

    //bootstrap-treeview 数据结构
    public class MenuTreeNode
    {
        public string text { get; set; }
        public List<MenuTreeNode> nodes { get; set; }
        public MenuTreeState state { get; set; }
    }

    public class MenuTreeState
    {
        public bool checkedd { get; set; }
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
    }
}