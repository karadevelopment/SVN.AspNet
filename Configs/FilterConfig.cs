using System.Web.Mvc;

namespace SVN.AspNet.Configs
{
    internal static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}