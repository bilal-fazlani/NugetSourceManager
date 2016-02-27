using System.Xml.Serialization;

namespace NugetSourceManager.Serialization.Entries
{
    [XmlRoot(ElementName = "add")]
    public class StringConfigEntry    
    {
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}