using System.Xml.Serialization;

namespace NugetSourceManager.Serialization.Entries
{
    [XmlRoot(ElementName = "add")]
    public class DisabledSourceEntry
    {
        [XmlAttribute(AttributeName = "key")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public bool Disabled { get; set; } = true;
    }
}