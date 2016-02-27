using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "activePackageSource")]
    public class ActivePackageSource
    {
        [XmlElement(ElementName = "add")]
        public PackageSource PackageSource { get; set; }
    }
}