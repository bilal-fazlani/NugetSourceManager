using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "packageRestore")]
    public class PackageRestore
    {
        [XmlElement(ElementName = "add")]
        public List<StringConfigEntry> Add { get; set; }
    }
}