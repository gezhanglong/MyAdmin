using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MyProject.Controllers
{
    public static class FormsAuthService
    {
        public static void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public static bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static IIdentity GetCurrentIdentity()
        {
            return HttpContext.Current.User.Identity;
        } 
    }
}