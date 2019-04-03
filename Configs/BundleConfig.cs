using SVN.AspNet.Engines;
using SVN.Reflection.Helpers;
using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Optimization;

namespace SVN.AspNet.Configs
{
    internal static class BundleConfig
    {
        private const string ASSET_FONT_DIRECTORY = "Fonts";
        private const string ASSET_IMAGES_DIRECTORY = "Images";
        private const string ASSET_SCRIPT_FILE = "Scripts.js";
        private const string ASSET_STYLE_FILE = "Styles.css";

        private const string ASSET_DIRECTORY_VARIABLE_FONT = "{SVN.AspNet.AssetPath.Fonts}";
        private const string ASSET_DIRECTORY_VARIABLE_IMAGES = "{SVN.AspNet.AssetPath.Images}";

        private static readonly string[] AssetStyles = new string[]
        {
            "Assets.Styles.FontAwesome",
            "Assets.Styles.Bootstrap",
            "Assets.Styles.KendoUI",
            "Assets.Styles.DataTable",
            "Assets.Styles.SVN",
        };

        private static readonly string[] AssetScripts = new string[]
        {
            "Assets.Scripts.jQuery",
            "Assets.Scripts.Popper",
            "Assets.Scripts.Bootstrap",
            "Assets.Scripts.Date",
            "Assets.Scripts.Highcharts",
            "Assets.Scripts.HighchartsThemes",
            "Assets.Scripts.Knockout",
            "Assets.Scripts.KendoUI",
            "Assets.Scripts.DataTable",
            "Assets.Scripts.SVN",
        };

        private static string AssetPath
        {
            get => Path.Combine(Engine.ViewsSharedPath, Assembly.GetCallingAssemblyName(), Assembly.GetCallingAssemblyVersion());
        }

        private static string AssetPathFonts
        {
            get => Path.Combine(BundleConfig.AssetPath, BundleConfig.ASSET_FONT_DIRECTORY);
        }

        private static string AssetPathImages
        {
            get => Path.Combine(BundleConfig.AssetPath, BundleConfig.ASSET_IMAGES_DIRECTORY);
        }

        private static string AssetPathScripts
        {
            get => Path.Combine(BundleConfig.AssetPath, BundleConfig.ASSET_SCRIPT_FILE);
        }

        private static string AssetPathStyles
        {
            get => Path.Combine(BundleConfig.AssetPath, BundleConfig.ASSET_STYLE_FILE);
        }

        private static string AssetUrl
        {
            get => $"/{BundleConfig.AssetPath.Replace('\\', '/')}";
        }

        public static string AssetUrlScripts
        {
            get => $"{BundleConfig.AssetUrl}/{BundleConfig.ASSET_SCRIPT_FILE}";
        }

        public static string AssetUrlStyles
        {
            get => $"{BundleConfig.AssetUrl}/{BundleConfig.ASSET_STYLE_FILE}";
        }

        private static void WriteResource(string filePath, Stream stream)
        {
            if (File.Exists(filePath))
            {
                return;
            }

            using (var fileStream = File.OpenWrite(filePath))
            {
                stream.CopyTo(fileStream);
            }
        }

        private static void WriteFonts()
        {
            var directoryPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, BundleConfig.AssetPathFonts);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var (filename, stream) in Assembly.GetResources(Assembly.GetCallingAssemblyName(), "Assets.Fonts"))
            {
                var filePath = Path.Combine(directoryPath, filename);
                BundleConfig.WriteResource(filePath, stream);
            }
        }

        private static void WriteImages()
        {
            var directoryPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, BundleConfig.AssetPathImages);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var (filename, stream) in Assembly.GetResources(Assembly.GetCallingAssemblyName(), "Assets.Images"))
            {
                var filePath = Path.Combine(directoryPath, filename);
                BundleConfig.WriteResource(filePath, stream);
            }
        }

        private static void WriteStyles()
        {
            var filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, BundleConfig.AssetPathStyles);

            if (File.Exists(filePath))
            {
                return;
            }

            var result = string.Empty;
            foreach (var folder in BundleConfig.AssetStyles)
            {
                foreach (var (filename, stream) in Assembly.GetResources(Assembly.GetCallingAssemblyName(), folder))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var content = string.Empty;
                        content += reader.ReadToEnd().Replace(BundleConfig.ASSET_DIRECTORY_VARIABLE_FONT, BundleConfig.AssetPathFonts.Replace('\\', '/'));
                        content += reader.ReadToEnd().Replace(BundleConfig.ASSET_DIRECTORY_VARIABLE_IMAGES, BundleConfig.AssetPathImages.Replace('\\', '/'));
                        result += $"{Environment.NewLine}{content}{Environment.NewLine}";
                    }
                }
            }

            File.WriteAllText(filePath, result);
        }

        private static void WriteScripts()
        {
            var filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, BundleConfig.AssetPathScripts);

            if (File.Exists(filePath))
            {
                return;
            }

            var result = string.Empty;
            foreach (var folder in BundleConfig.AssetScripts)
            {
                foreach (var (filename, stream) in Assembly.GetResources(Assembly.GetCallingAssemblyName(), folder))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var content = string.Empty;
                        content += reader.ReadToEnd().Replace(BundleConfig.ASSET_DIRECTORY_VARIABLE_FONT, BundleConfig.AssetPathFonts.Replace('\\', '/'));
                        content += reader.ReadToEnd().Replace(BundleConfig.ASSET_DIRECTORY_VARIABLE_IMAGES, BundleConfig.AssetPathImages.Replace('\\', '/'));
                        result += $"{Environment.NewLine}{content}{Environment.NewLine}";
                    }
                }
            }

            File.WriteAllText(filePath, result);
        }

        public static void Init(BundleCollection collection)
        {
            BundleConfig.WriteFonts();
            BundleConfig.WriteImages();
            BundleConfig.WriteStyles();
            BundleConfig.WriteScripts();
        }
    }
}