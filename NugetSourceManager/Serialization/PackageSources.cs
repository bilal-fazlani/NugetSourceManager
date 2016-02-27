using System.Collections.Generic;
using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "packageSources")]
    public class PackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Entries { get; set; }
    }
}