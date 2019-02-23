using SVN.AspNet.Engines;
using SVN.Core.Linq;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SVN.AspNet.Configs
{
    internal static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var route = Engine.RouteDefault.Split("/");
            var routeDefaultController = route.First();
            var routeDefaultAction = route.Last();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = routeDefaultController, action = routeDefaultAction, id = UrlParameter.Optional }
            );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}