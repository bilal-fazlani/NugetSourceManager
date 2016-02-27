using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "apikeys")]
    public class Apikeys
    {
        [XmlElement(ElementName = "add")]
        public List<StringConfigEntry> Add { get; set; }
    }
}