using System;
using System.Linq;
using System.Web.Mvc;
using MyProject.Tasks;
using MyProject.Services.Mappers;
using MyProject.Services.Utility;
using MyProject.Services.MvcPager;
using MyProject.Services.Extensions;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Matrix.Controllers.AdminUsers.ViewModels; 
using MyProject.Matrix.Controllers.Core;

namespace MyProject.Matrix.Controllers.AdminUsers
{
    public class AdminUsersController : BaseController
    {
        private readonly AdminUserRoleTask _adminUserRoleTask = new AdminUserRoleTask();
        private readonly AdminUserTask _adminUserTask = new AdminUserTask();

        #region 查看用户列表 List
        [SupportFilter(ActionName="Index")]
        public ActionResult List(string account,int? userRole,int pageIndex = 1,int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var roleList = _adminUserRoleTask.GetAll().ToSelectList(c => c.RoleId.ToString(), c => c.RoleName);

            roleList.Insert(0, new SelectListItem {Text = "全部", Value = string.Empty});

            ViewBag.RoleList = roleList;

            var pagedList = _adminUserTask.GetPagedList(account, userRole, pageIndex, pageSize);

            var items = pagedList.Select(EntityMapper.Map<AdminUserDto, AdminUserModel>);

            var model = new PagedList<AdminUserModel>(items, pageIndex, pageSize, pagedList.TotalItemCount);

            return View(model);
        }

        #endregion

        #region 保存/修改用户信息 Save
        [SupportFilter]
        public ActionResult Save(int? id)
        {
            ViewBag.RoleList = _adminUserRoleTask.GetAll().ToSelectList(c => c.RoleId.ToString(), c => c.RoleName);

            var model = new SaveAdminUserModel();

            if(id != null)
            {
                var item = _adminUserTask.GetById((int) id);
                if (item == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model = EntityMapper.Map<AdminUser, SaveAdminUserModel>(item);
            }
            return View(model);
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult Save(SaveAdminUserModel savemodel)
        {
            ViewBag.RoleList = _adminUserRoleTask.GetAll().ToSelectList(c => c.RoleId.ToString(), c => c.RoleName);
            if (savemodel.AdminUserId == null)
            {
                if (savemodel.Password != savemodel.PasswordTwo)
                {
                    ModelState.AddModelError("PasswordTwo", "密码不一致");
                    return View(savemodel);
                }
                var user = _adminUserTask.GetByUserName(savemodel.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "用户名称已注册");
                    return View(savemodel);
                }
                if (ModelState.IsValid)
                {
                    var model = new AdminUser
                    {
                        UserName = savemodel.UserName,
                        Password = CryptTools.HashPassword(savemodel.Password),
                        IsLock = false,
                        RoleId = savemodel.RoleId
                    };
                    _adminUserTask.Add(model);

                    return CloseParentBox("保存成功", "/AdminUsers/List");
                }
            }
            else
            {
                var model = _adminUserTask.GetById((int)savemodel.AdminUserId);

                if (model == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model.RoleId = savemodel.RoleId;

                _adminUserTask.Update(model);
                return CloseParentBox("修改成功", "/AdminUsers/List");
            }
            return View(savemodel);
        }

        #endregion

        #region 删除用户信息 Delete
        [SupportFilter]
        [HttpPost]
        public void Delete(int id)
        {
            _adminUserTask.Delete(id);
        }

        #endregion

        #region 锁定用户 Lock
        [SupportFilter]
        public ActionResult Lock(int id)
        {
            var model = _adminUserTask.GetById(id);
            if(model != null)
            {
                model.IsLock = true;
                _adminUserTask.Update(model);
            }
            return AlertMsg("锁定成功", HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        #endregion

        #region 用户解锁 UnLock
        [SupportFilter(ActionName = "Lock")]
        public ActionResult UnLock(int id)
        {
            var model = _adminUserTask.GetById(id);
            if(model != null)
            {
                model.IsLock = false;
                _adminUserTask.Update(model);
            }
            return AlertMsg("解锁成功", HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        #endregion


        #region 修改密码
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        { 
            var name = GetCurrentAdmin();
            var user = _adminUserTask.GetByUserName(name);
            if (user.Password != CryptTools.HashPassword(model.OldPw))
            {
                ModelState.AddModelError("OldPw", "旧密码不正确");
            }
            if (model.NewPw != model.AgainPw)
            {
                ModelState.AddModelError("AgainPw", "两次密码不一致");
            }
            if (ModelState.IsValid)
            {
                user.Password = CryptTools.HashPassword(model.AgainPw);
                _adminUserTask.Update(user);
                return CloseParentBox("修改成功！", "/Home/LogInIndex");
            }
            return View();
        } 
        #endregion

       
    }
}
