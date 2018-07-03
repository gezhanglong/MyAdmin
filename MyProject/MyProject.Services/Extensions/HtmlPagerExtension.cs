using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MyProject.Services.Extensions
{
    public static class PagerHelper
    {
        private const int DefaultDisplayCount = 11;

        /// <summary>   
        /// Pager Helper Extensions   
        /// </summary>   
        /// <param name="htmlHelper"></param>   
        /// <param name="currentPage">当前页码</param>   
        /// <param name="pageSize">页面显示的数据条目</param>   
        /// <param name="totalCount">总记录数</param>   
        /// <param name="toDisplayCount">Helper要显示的页数</param>   
        /// <returns></returns>   
        public static string Pager(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount, int toDisplayCount)
        {
            var pager = new RenderPager(htmlHelper.ViewContext, currentPage, pageSize, totalCount, toDisplayCount);

            return pager.RenderHtml();
        }

        /// <summary>   
        /// Pager Helper Extensions   
        /// </summary>   
        /// <param name="htmlHelper"></param>   
        /// <param name="currentPage">当前页码</param>   
        /// <param name="pageSize">页面显示的数据条目</param>   
        /// <param name="totalCount">总记录数</param>   
        /// <returns></returns>   
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var pager = new RenderPager(htmlHelper.ViewContext, currentPage, pageSize, totalCount, DefaultDisplayCount);

            var str = pager.RenderHtml();
            return MvcHtmlString.Create(str);
        }
    }

    public class RenderPager
    {
        #region 字段

        /// <summary>   
        /// 当前页面的ViewContext   
        /// </summary>   
        private ViewContext _viewContext;
        /// <summary>   
        /// 当前页码   
        /// </summary>   
        private readonly int _currentPage;
        /// <summary>   
        /// 页面要显示的数据条数   
        /// </summary>   
        private readonly int _pageSize;
        /// <summary>   
        /// 总的记录数   
        /// </summary>   
        private readonly int _totalCount;
        /// <summary>   
        /// Pager Helper 要显示的页数   
        /// </summary>   
        private readonly int _toDisplayCount;

        private readonly string _pagelink;

        #endregion

        #region 构造函数

        public RenderPager(ViewContext viewContext, int currentPage, int pageSize, int totalCount, int toDisplayCount)
        {
            this._viewContext = viewContext;
            this._currentPage = currentPage;
            this._pageSize = pageSize;
            this._totalCount = totalCount;
            this._toDisplayCount = toDisplayCount;


            var reqUrl = viewContext.RequestContext.HttpContext.Request.RawUrl;
            var link = "";

            var re = new Regex(@"page=(\d+)|page=", RegexOptions.IgnoreCase);

            var results = re.Matches(reqUrl);

            if (results.Count > 0)
            {
                link = reqUrl.Replace(results[0].ToString(), "page=[%page%]");
            }
            else if (reqUrl.IndexOf("?") < 0)
            {
                link = reqUrl + "?page=[%page%]";
            }
            else
            {
                link = reqUrl + "&page=[%page%]";
            }
            this._pagelink = link;
        }

        #endregion

        #region 方法

        public string RenderHtml()
        {
            if (_totalCount <= _pageSize)
                return string.Empty;

            //总页数   
            var pageCount = (int)Math.Ceiling(this._totalCount / (double)this._pageSize);

            //起始页   
            int start = 1;

            //结束页   
            int end = _toDisplayCount;
            if (pageCount < _toDisplayCount) end = pageCount;

            //中间值   
            int centerNumber = _toDisplayCount / 2;

            if (pageCount > _toDisplayCount)
            {

                //显示的第一位   
                int topNumber = _currentPage - centerNumber;

                if (topNumber > 1)
                {
                    start = topNumber;
                }

                if (topNumber > pageCount - _toDisplayCount)
                {
                    start = pageCount - _toDisplayCount + 1;
                }

                //显示的最后一位   
                int endNumber = _currentPage + centerNumber;

                if (_currentPage >= pageCount - centerNumber)
                {
                    end = pageCount;
                }
                else
                {
                    if (endNumber > _toDisplayCount)
                    {
                        end = endNumber;
                    }
                }

            }

            var sb = new StringBuilder();

            //Previous   
            if (this._currentPage > 1)
            {
                sb.Append(GeneratePageLink("< 上一页", this._currentPage - 1));
            }

            if (start > 1)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append("...");
            }

            //end Previous   

            for (int i = start; i <= end; i++)
            {
                if (i == this._currentPage)
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(), i));
                }
            }

            //Next   
            if (end < pageCount)
            {
                sb.Append("...");
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
            }

            if (this._currentPage < pageCount)
            {
                sb.Append(GeneratePageLink("下一页 >", this._currentPage + 1));
            }
            //end Next   

            //sb.Append("<span><input type=\"text\" class=\"pagerInput\" id=\"pagerInput\" maxlength=\"4\" onkeypress=\"return event.keyCode>=48&&event.keyCode<=57\"/></span>");
            //sb.Append("<span><input type=\"button\" value=\"跳转\" class=\"pagerButton\" onclick=\"if (!isNaN(parseInt(pagerInput.value))) { window.location='" + this._pagelink.Replace("[%page%]", "") + "'+pagerInput.value;}\"/></span>");

            return sb.ToString();
        }

        /// <summary>   
        /// 生成Page的链接   
        /// </summary>   
        /// <param name="linkText">文字</param>   
        /// <param name="pageNumber">页码</param>   
        /// <returns></returns>   
        private string GeneratePageLink(string linkText, int pageNumber)
        {

            string linkFormat = string.Format("<a href=\"{0}\">{1}</a>", this._pagelink.Replace("[%page%]", pageNumber.ToString()), linkText);

            return linkFormat;

        }

        #endregion
    }
}
