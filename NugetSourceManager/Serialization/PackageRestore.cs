using System.Collections.Generic;
using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "packageRestore")]
    public class PackageRestore
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Add { get; set; }
    }
}