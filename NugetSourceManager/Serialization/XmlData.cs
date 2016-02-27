using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
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
