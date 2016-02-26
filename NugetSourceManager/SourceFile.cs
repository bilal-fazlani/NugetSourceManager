using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FluentAssertions.Common;

namespace NugetSourceManager
{
    public abstract class SourceFile
    {
        public string Path { get; protected set; }

        public XmlData XmlData { get; protected set; }

        public void AddOrUpdateSource(PackageSource packageSource)
        {
            AddOrUpdateSource(packageSource.Name, packageSource.SourcePath, packageSource.ProtocolVersion);
        }

        public void AddOrUpdateSource(string name, string sourcePath, string protocalVersion = null)
        {
            PackageSource newPackageSource = new PackageSource
            {
                Name = name,
                SourcePath = sourcePath,
                ProtocolVersion = protocalVersion
            };

            List<PackageSource> existingPackageSources = XmlData.PackageSources.Sources
                .Where(x => x.Equals(newPackageSource))
                .ToList();

            if (existingPackageSources.Any()) //update
            {
                if (existingPackageSources.Count == 1)
                {
                    existingPackageSources[0].Name = name;
                    existingPackageSources[0].SourcePath = sourcePath;
                    existingPackageSources[0].ProtocolVersion = protocalVersion;
                }
                else
                {
                    foreach (var source in existingPackageSources)
                    {
                        XmlData.PackageSources.Sources.Remove(source);
                    }

                    XmlData.PackageSources.Sources.Add(newPackageSource);
                }
            }
            else
            {
                XmlData.PackageSources.Sources.Add(newPackageSource);
            }
        }

        protected void LoadXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlData));
            using (var filestream = new FileStream(Path, FileMode.Open))
            {
                XmlData xmlData = (XmlData)serializer.Deserialize(new StreamReader(filestream));
                XmlData = xmlData;
            }
        }
    }

    public class DefaultSourceFile : SourceFile
    {
        private static readonly DefaultSourceFile Instance = new DefaultSourceFile();

        public static DefaultSourceFile GetInstace()
        {
            return Instance;
        }

        private DefaultSourceFile()
        {
            SetDefaultPath();
            LoadXml();
        }

        private void SetDefaultPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string nugetPath = System.IO.Path.Combine(appData, "Nuget", "Nuget.Config");
            if (File.Exists(nugetPath)) this.Path = nugetPath;
            else throw new FileNotFoundException("Default Nuget.Config file not found", nugetPath);
        }
    }

    public class CustomSourceFile : SourceFile
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