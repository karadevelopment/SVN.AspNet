using Newtonsoft.Json;
using SVN.AspNet.Configs;
using SVN.AspNet.Engines;
using SVN.Core.Linq;
using SVN.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SVN.AspNet.Models
{
    public class ViewModel : Controller
    {
        private string RouteController { get; set; }
        private string RouteAction { get; set; }
        private string RouteId { get; set; }

        public string AssetScript { get; private set; }
        public string AssetStyle { get; private set; }

        public string SharedScript { get; private set; }
        public string SharedStyle { get; private set; }

        public string RouteScript { get; private set; }
        public string RouteStyle { get; private set; }

        public object Route
        {
            get => new
            {
                controller = this.RouteController,
                action = this.RouteAction,
                id = this.RouteId,
            };
        }

        public string RouteString
        {
            get => $"/{this.RouteController}/{this.RouteAction}";
        }

        protected string RootUrl
        {
            get => base.Request.Url.GetLeftPart(UriPartial.Authority);
        }

        protected Dictionary<string, string> UrlParameters
        {
            get
            {
                var query = base.Request.QueryString.ToString();

                var result = query.Split('&').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x =>
                {
                    if (x.Contains('='))
                    {
                        return x;
                    }
                    else
                    {
                        return $"{x}=";
                    }
                }).Select(x => new
                {
                    key = x.Substring(0, x.IndexOf("=")),
                    value = x.Remove(0, x.IndexOf("=") + 1),
                });

                return result.ToDictionary(x => x.key, x => x.value);
            }
        }

        protected ViewModel()
        {
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            this.RouteController = context.RouteData.Values["controller"] as string;
            this.RouteAction = context.RouteData.Values["action"] as string;
            this.RouteId = context.RouteData.Values["id"] as string;

            this.AssetScript = $"<script type='text/javascript' src='{BundleConfig.AssetUrlScripts}'></script>";
            this.AssetStyle = $"<link rel='stylesheet' type='text/css' href='{BundleConfig.AssetUrlStyles}'>";

            this.SharedScript = ViewModel.ReadResources(Engine.ViewsSharedPath, $"{Engine.ViewsSharedLayoutViewName}.js").Join(Environment.NewLine);
            this.SharedStyle = ViewModel.ReadResources(Engine.ViewsSharedPath, $"{Engine.ViewsSharedLayoutViewName}.css").Join(Environment.NewLine);

            this.RouteScript = ViewModel.ReadResources($@"{Engine.ViewsPath}\{this.RouteController}", $"{this.RouteAction}.js").Join(Environment.NewLine);
            this.RouteStyle = ViewModel.ReadResources($@"{Engine.ViewsPath}\{this.RouteController}", $"{this.RouteAction}.css").Join(Environment.NewLine);

            base.OnActionExecuting(context);
        }

        private static IEnumerable<string> ReadResources(string relativePath, string searchPattern)
        {
            var path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, relativePath);

            if (Directory.Exists(path))
            {
                foreach (var resource in Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories))
                {
                    yield return $"{Environment.NewLine}{System.IO.File.ReadAllText(resource)}{Environment.NewLine}";
                }
            }
        }

        protected T GetCookie<T>(string key)
        {
            var cookie = base.Request.Cookies[key];
            if (cookie is null)
            {
                return default(T);
            }
            var cookieValue = cookie.Value.Decrypt();
            if (cookieValue == "null")
            {
                return default(T);
            }
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };
            return JsonConvert.DeserializeObject<T>(cookieValue, settings);
        }

        protected string GetCookie(string key)
        {
            var result = this.GetCookie<string>(key);

            if (result is null)
            {
                return string.Empty;
            }

            return result;
        }

        protected void SetCookie<T>(string key, T value)
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };
            var cookieValue = JsonConvert.SerializeObject(value, settings).Encrypt();
            var cookie = new HttpCookie(key)
            {
                Value = cookieValue,
                Expires = DateTime.Now.AddDays(30),
            };
            base.Response.Cookies.Set(cookie);
        }

        protected void SetCookie(string key)
        {
            this.SetCookie<object>(key, null);
        }

        protected void SetCookie(string key, string value)
        {
            this.SetCookie<string>(key, value);
        }

        private IEnumerable<string> GetRenderText()
        {
            yield return this.AssetScript;
            yield return this.AssetStyle;

            yield return "<script>";
            yield return $"window.route = {System.Web.Helpers.Json.Encode(this.Route)};";
            yield return "window.urlquery = {" + this.UrlParameters.Select(x => $"{x.Key}: {x.Value}").Join(", ") + "};";
            yield return "setQuery = function ()";
            yield return "{";
            yield return "let properties = [];";
            yield return "for (let key in window.urlquery)";
            yield return "{";
            yield return "properties.push(key + '=' + window.urlquery[key]);";
            yield return "}";
            yield return "if (0 < properties.length)";
            yield return "{";
            yield return "window.history.pushState('', '', '?' + properties.join('&'));";
            yield return "}";
            yield return "};";
            yield return "setQuery();";
            yield return "</script>";

            yield return "<script>";
            yield return this.RouteScript;
            yield return this.SharedScript;
            yield return "</script>";

            yield return "<style>";
            yield return this.SharedStyle;
            yield return this.RouteStyle;
            yield return "</style>";
        }

        public IHtmlString Render()
        {
            return new HtmlString(this.GetRenderText().Join(Environment.NewLine));
        }
    }
}