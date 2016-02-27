using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Xml.Serialization;

namespace NugetSourceManager
{
    [XmlRoot(ElementName = "add")]
    public class PackageSource
    {
        public PackageSource()
        {
            
        }

        public PackageSource(string name, string sourcePath, string protocolVersion = null)
        {
            Name = name;
            SourcePath = sourcePath;
            ProtocolVersion = protocolVersion;
        }

        [XmlAttribute(AttributeName = "key")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string SourcePath { get; set; }

        [XmlAttribute(AttributeName = "protocolVersion")]
        public string ProtocolVersion { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as PackageSource) != null)
            {
                PackageSource otherSource = (PackageSource)obj;

                bool nameEquals = string.Compare(otherSource.Name, Name, StringComparison.OrdinalIgnoreCase) == 0;

                bool urlEquals = string.Compare(this.SourcePath, otherSource.SourcePath,
                    StringComparison.OrdinalIgnoreCase) == 0;

                return nameEquals | urlEquals;
            }

            if ((obj as string) != null)
            {
                string nameOrSource = (string) obj;
                return string.Compare(Name, nameOrSource, StringComparison.OrdinalIgnoreCase) == 0 ||
                       string.Compare(SourcePath, nameOrSource, StringComparison.OrdinalIgnoreCase) == 0;
            }

            return false;
        }
    }

    [XmlRoot(ElementName = "packageSources")]
    public class PackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Entries { get; set; }
    }

    [XmlRoot(ElementName = "disabledPackageSources")]
    public class DisabledPackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<DisabledSourceEntry> Entries { get; set; }

        public bool Contains(string name)
        {
            return Entries.Any(x => 
                string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void Add(string name)
        {
            if (!Contains(name))
            {
                Entries.Add(new DisabledSourceEntry
                {
                    Name = name
                });
            }
            else
            {
                Entries
                    .Single(x =>
                        string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0)
                    .Disabled = true;
            }
        }

        public void Remove(string name)
        {
            if (Contains(name))
            {
                var entry = Entries
                    .Single(x =>
                        string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
                Entries.Remove(entry);
            }
        }
    }

    [XmlRoot(ElementName = "add")]
    public class DisabledSourceEntry
    {
        [XmlAttribute(AttributeName = "key")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public bool Disabled { get; set; } = true;
    }

    [XmlRoot(ElementName = "packageRestore")]
    public class PackageRestore
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Add { get; set; }
    }

    [XmlRoot(ElementName = "bindingRedirects")]
    public class BindingRedirects
    {
        [XmlElement(ElementName = "add")]
        public PackageSource PackageSource { get; set; }
    }

    [XmlRoot(ElementName = "apikeys")]
    public class Apikeys
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Add { get; set; }
    }

    [XmlRoot(ElementName = "activePackageSource")]
    public class ActivePackageSource
    {
        [XmlElement(ElementName = "add")]
        public PackageSource PackageSource { get; set; }
    }

    [XmlRoot(ElementName = "configuration")]
    public class XmlData
    {
        [XmlElement(ElementName = "packageSources")]
        public PackageSources PackageSources { get; set; }
        [XmlElement(ElementName = "disabledPackageSources")]
        public DisabledPackageSources DisabledPackageSources { get; set; }
        [XmlElement(ElementName = "packageRestore")]
        public PackageRestore PackageRestore { get; set; }
        [XmlElement(ElementName = "bindingRedirects")]
        public BindingRedirects BindingRedirects { get; set; }
        [XmlElement(ElementName = "apikeys")]
        public Apikeys Apikeys { get; set; }
        [XmlElement(ElementName = "activePackageSource")]
        public ActivePackageSource ActivePackageSource { get; set; }
    }

}
