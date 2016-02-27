using System.Collections.Generic;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "bindingRedirects")]
    public class BindingRedirects
    {
        [XmlElement(ElementName = "add")]
        public List<StringConfigEntry> Entries { get; set; }
    }
}