using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "bindingRedirects")]
    public class BindingRedirects
    {
        [XmlElement(ElementName = "add")]
        public PackageSource PackageSource { get; set; }
    }
}