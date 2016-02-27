using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "config")]
    public class Config
    {
        [XmlElement(ElementName = "add")]
        public List<StringConfigEntry> Entries { get; set; } 
    }
}