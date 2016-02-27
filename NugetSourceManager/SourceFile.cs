﻿using System;
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

        protected void Save()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(typeof(XmlData));
            using (var filestream = new FileStream(Path, FileMode.Create))
            {
                serializer.Serialize(filestream, XmlData, ns);
            }
        }

        public void AddOrUpdateSource(string name, string sourcePath, string protocalVersion = null)
        {
            PackageSource newPackageSource = new PackageSource
            {
                Name = name,
                SourcePath = sourcePath,
                ProtocolVersion = protocalVersion
            };

            List<PackageSource> existingPackageSources = XmlData.PackageSources.Entries
                .Where(x => x.Equals(newPackageSource))
                .ToList();

            if (existingPackageSources.Any()) //update
            {
                if (existingPackageSources.Count == 1)
                {
                    //first update the disable section for name
                    UpdateDisabledSectionForName(existingPackageSources[0].Name, name);

                    existingPackageSources[0].Name = name;
                    existingPackageSources[0].SourcePath = sourcePath;
                    existingPackageSources[0].ProtocolVersion = protocalVersion;
                }
                else
                {
                    foreach (var source in existingPackageSources)
                    {
                        UpdateDisabledSectionForName(source.Name, name);
                        XmlData.PackageSources.Entries.Remove(source);
                    }

                    XmlData.PackageSources.Entries.Add(newPackageSource);
                }
            }
            else
            {
                XmlData.PackageSources.Entries.Add(newPackageSource);
            }

            Save();
        }

        protected void LoadXml()
        {
            CreateFileIfNotExists();

            XmlSerializer serializer = new XmlSerializer(typeof(XmlData));
            using (var filestream = new FileStream(Path, FileMode.Open))
            {
                XmlData xmlData = (XmlData)serializer.Deserialize(new StreamReader(filestream));
                XmlData = xmlData;
            }
        }

        private void CreateFileIfNotExists()
        {
            string directory = System.IO.Path.GetDirectoryName(Path);

            bool dirExists = Directory.Exists(directory);

            if (!dirExists)
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(Path))
            {
                File.Copy("DefaultNuget.Config", Path);
            }
        }

        public void RemovePackageSource(string packageSourceNameOrPath)
        {
            string sourceIdentifier = packageSourceNameOrPath;

            try
            {
                sourceIdentifier = GetName(packageSourceNameOrPath);
            }
            catch (InvalidOperationException)
            {
            }

            XmlData.PackageSources.Entries
                    .RemoveAll(x => x.Equals(sourceIdentifier));

            XmlData.DisabledPackageSources.Remove(sourceIdentifier);

            Save();
        }

        public PackageSource GetPackageSource(string packageSourceNameOrPath)
        {
            return XmlData.PackageSources.Entries.SingleOrDefault(
                    x => x.Equals(packageSourceNameOrPath)
                );
        }

        /// <summary>
        /// Disable an existing package
        /// </summary>
        /// <param name="packageSourceNameOrPath"></param>
        /// <exception cref="InvalidOperationException">When the given package does not exist</exception>
        public void DisablePackageSource(string packageSourceNameOrPath)
        {
            string name = GetName(packageSourceNameOrPath);

            if(!string.IsNullOrEmpty(name))
                XmlData.DisabledPackageSources.Add(name);

            Save();
        }

        private void UpdateDisabledSectionForName(string oldName, string newName)
        {
            var matchingEntry = XmlData.DisabledPackageSources.Entries.SingleOrDefault(x =>
                string.Compare(x.Name, oldName, StringComparison.OrdinalIgnoreCase) == 0);

            if (matchingEntry != null) matchingEntry.Name = newName;
        }

        private string GetName(string packageSourceNameOrPath)
        {
            PackageSource packageSource = GetPackageSource(packageSourceNameOrPath);

            if (packageSource != null)
            {
                return packageSource.Name;
            }

            throw new InvalidOperationException($@"A source with name or source path '{packageSourceNameOrPath}' 
does not exist");
        }

        public bool IsSourceDisabled(string packageSourceNameOrPath)
        {
            var name = GetName(packageSourceNameOrPath);
            return XmlData.DisabledPackageSources.Contains(name);
        }

        public void EnablePackageSource(string packageSourceNameOrPath)
        {
            string name = GetName(packageSourceNameOrPath);

            XmlData.DisabledPackageSources.Remove(name);

            Save();
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
            this.Path = nugetPath;
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