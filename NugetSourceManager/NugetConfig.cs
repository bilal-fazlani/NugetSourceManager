using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Xml.Serialization;

namespace NugetSourceManager
{
    [XmlRoot(ElementName = "add")]
    public class PackageSource
    {
        [XmlAttribute(AttributeName = "key")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string SourcePath { get; set; }

        [XmlAttribute(AttributeName = "protocolVersion")]
        public string ProtocolVersion { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            PackageSource otherSource = (PackageSource) obj;

            bool nameEquals = string.Compare(otherSource.Name, Name, StringComparison.OrdinalIgnoreCase) == 0;

            //Uri currentUri = new Uri(SourcePath);
            //Uri otherUri = new Uri(otherSource.SourcePath);

            bool urlEquals = string.Compare(this.SourcePath, otherSource.SourcePath, 
                StringComparison.OrdinalIgnoreCase) == 0;

            return nameEquals | urlEquals;
        }
    }

    [XmlRoot(ElementName = "packageSources")]
    public class PackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Sources { get; set; }
    }

    [XmlRoot(ElementName = "disabledPackageSources")]
    public class DisabledPackageSources
    {
        [XmlElement(ElementName = "add")]
        public PackageSource PackageSource { get; set; }
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
