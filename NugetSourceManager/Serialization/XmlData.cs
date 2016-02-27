using System.Xml.Serialization;
using NugetSourceManager.Serialization.Sections;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "configuration")]
    public class XmlData
    {
        [XmlElement(ElementName = "config")]
        public Config Config { get; set; }

        [XmlElement(ElementName = "packageSources")]
        public PackageSources PackageSources { get; set; } = new PackageSources();
        [XmlElement(ElementName = "disabledPackageSources")]
        public DisabledPackageSources DisabledPackageSources { get; set; } = new DisabledPackageSources();
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
