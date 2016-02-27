using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "packageSources")]
    public class PackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSourceEntry> Entries { get; set; } = new List<PackageSourceEntry>();
    }
}