using MyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.Core
{
    public static  class ExtendMvcHtml
    {
        /// <summary>
        /// 权限按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="perm">权限列表</param>
        /// <param name="keycode">操作码</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string text, List<PermDto> perm, string keycode, string href, string onclick)
        {
            if (perm.Where(a => a.Action == keycode).Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<a class=\"badge badge-info\" href=\"{0}\" onclick=\"{1}\" id=\"{2}\">{3}</a>", href, onclick, id, text);   
                return new MvcHtmlString(sb.ToString());
            }
            else
            {
                return new MvcHtmlString("");
            }
        }

        
        /// <summary>
        /// 普通按钮
        /// </summary>
        /// <param name="helper">htmlhelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="icon">控件icon图标class</param>
        /// <param name="text">控件的名称</param>
        /// <param name="hr">分割线</param>
        /// <returns>html</returns>
        public static MvcHtmlString ToolButton(this HtmlHelper helper, string id, string text, string href, string onclick)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href=\"{0}\" onclick=\"{1}\" id=\"{2}\">{3}</a>", href, onclick, id, text);
            return new MvcHtmlString(sb.ToString()); 

        }
    }
}