using System.Collections.Generic;
using System.IO;

namespace NugetSourceManager.SourceFile
{
    public class CustomSourceFile : SourceFileBase
    {
        private static readonly Dictionary<string, CustomSourceFile> Instances = new Dictionary<string, CustomSourceFile>();

        public static CustomSourceFile GetInstace(string path)
        {
            return Instances.ContainsKey(path) ? Instances[path] : AddCustomSourceFileInstance(path);
        }

        private static CustomSourceFile AddCustomSourceFileInstance(string path)
        {
            var instance = new CustomSourceFile(path);
            Instances.Add(path, instance);
            return instance;
        }

        private CustomSourceFile(string path)
        {
            SetCustomPath(path);
            LoadXml();
        }

        private void SetCustomPath(string path)
        {
            if (File.Exists(path)) this.Path = path;
            else throw new FileNotFoundException("Nuget.Config file not found", path);
        }
    }
}