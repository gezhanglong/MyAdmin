using System.Web.Mvc;
using MyProject.Controllers.Account.ViewModels;
using MyProject.Tasks;
using MyProject.Services.Utility;
using System.Web;
using System.Web.SessionState;
using MyProject.Core.Dtos;
using MyProject.Task;
using System;
using MyProject.Core.Enum;
using MyProject.Services.Extensions;

namespace MyProject.Controllers.Account
{
    public class AccountController : Controller, IRequiresSessionState
    {
        private readonly AdminUserTask _adminUserTask = new AdminUserTask(); 

        #region 登录
        public ActionResult Index()
        {
            var model = new LogOnModel(); 
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        { 
            if (ModelState.IsValid)
            {
                var userPassword = _adminUserTask.GetByUserName(model.UserName);
                if (userPassword == null)
                    return AlertMsg("账号不存在", Request.UrlReferrer.PathAndQuery);
                if (userPassword.Password != CryptTools.HashPassword(model.Password))
                    return AlertMsg("账号或密码不正确", Request.UrlReferrer.PathAndQuery);
                if (userPassword.IsLock)
                    return AlertMsg("对不起，您的账号被锁定", Request.UrlReferrer.PathAndQuery);
                if (model.ValidationCode != Session["code"].ToString())
                    return AlertMsg("验证码不正确", Request.UrlReferrer.PathAndQuery); 
                AccountDto account = new AccountDto();
                account.AdminUserId = userPassword.AdminUserId;
                account.UserName = userPassword.UserName;
                account.RoleId = userPassword.RoleId;
                Session["Account"] = account;
                SysLogTask.AddLog(new MyProject.Core.Entities.SysLogDto() {  Message = "", Module = LogModuleEnum.Land, Type = LogTypeEnum.Land, Operator = userPassword.UserName,Result="登陆成功" });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        } 
        #endregion 

        #region 退出
        public ActionResult LogOut()
        {
            Session["Account"] = "";
            return RedirectToAction("Index", "Account");
        } 
        #endregion

        #region 辅助方法
        protected ActionResult AlertMsg(string msg, string returnUrl)
        {
            var script = string.Format("<script>alert('{0}');this.location.href='{1}';</script>", msg, returnUrl);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }
        #endregion

       
    }
}