using Hangfire.Dashboard;

namespace NbSites.Web.Helpers
{
    public class MyDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var isLocal = context.GetHttpContext().Request.IsLocal();
            if (isLocal)
            {
                return true;
            }

            var httpContext = context.GetHttpContext();
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (!httpContext.User.HasClaim("Role", "Admin"))
            {
                return false;
            }

            return true;
        }
    }
}
