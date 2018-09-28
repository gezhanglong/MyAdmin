using System.Web.Mvc;
using MyProject.Core.Entities;
using MyProject.Tasks;
using MyProject.Core.Dtos;
using System.Collections.Generic;
using System.Web;
using MyProject.Task;

namespace MyProject.Matrix.Controllers
{
    public class BaseController : Controller
    {
        private readonly AdminPowerTask _powersTask = new AdminPowerTask();
        private readonly AdminUserTask _adminUserTask = new AdminUserTask();

        //刷新本窗口
        protected ActionResult AlertMsg(string msg, string returnUrl)
        {
            var script = string.Format("<script>alert('{0}');this.location.href='{1}';</script>", msg, returnUrl);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        //alert
        protected ActionResult AlertMsg(string msg)
        {
            var script = string.Format("<script>alert('{0}');</script>", msg);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }
        
        //刷新父窗口
        protected ActionResult CloseParentBox(string msg, string returnUrl)
        {
            var script = string.Format("<script>alert('{0}');parent.location.href='{1}';</script>", msg, returnUrl);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        
        //整个窗口刷新
        public ActionResult AlertMsgTop(string msg,string returnUrl)
        {
            var script = string.Format("<script>alert('{0}');window.top.location.href='{1}';</script>", msg,returnUrl);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        /// <summary>
        /// 获得当前用户信息
        /// </summary>
        /// <returns></returns>
        public string  GetCurrentAdmin()
        {
            HttpCookie cookie = Request.Cookies["Account"];
            if (cookie == null)
            {
                return null;
            }
            return cookie.Value;
        }

        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns>权限列表</returns>
        public List<PermDto> GetPermission()
        {
            string filePath = HttpContext.Request.FilePath;

            List<PermDto> perm = (List<PermDto>)Session[filePath];
            return perm;
        }
 
    }
}
