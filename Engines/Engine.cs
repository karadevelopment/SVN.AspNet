using SVN.AspNet.Configs;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SVN.AspNet.Engines
{
    public class Engine
    {
        public static string RouteDefault { get; set; } = "/Home/Index";
        public static string ViewsPath { get; set; } = "Views";
        public static string ViewsSharedPath { get; set; } = @"Views\Shared";
        public static string ViewsSharedLayoutViewName { get; set; } = "Layout";

        public static void Initialize()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new Engine1());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.Init(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();
        }
    }
}