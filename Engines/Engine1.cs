using System.Web.Mvc;

namespace SVN.AspNet.Engines
{
    internal class Engine1 : RazorViewEngine
    {
        private string[] AreaFormats => new string[] { "~/Views/{1}/{0}.cshtml" };
        private string[] LayoutFormats => new string[] { "~/Views/Shared/{0}.cshtml" };
        private string[] ViewExtensions => new string[] { "cshtml" };

        public Engine1()
        {
            base.AreaMasterLocationFormats = this.AreaFormats;
            base.AreaPartialViewLocationFormats = this.AreaFormats;
            base.AreaViewLocationFormats = this.AreaFormats;
            base.ViewLocationFormats = this.AreaFormats;

            base.MasterLocationFormats = this.LayoutFormats;
            base.PartialViewLocationFormats = this.LayoutFormats;

            base.FileExtensions = this.ViewExtensions;
        }
    }
}