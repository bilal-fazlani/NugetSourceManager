using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "activePackageSource")]
    public class ActivePackageSource
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSourceEntry> Entries { get; set; }
    }
}