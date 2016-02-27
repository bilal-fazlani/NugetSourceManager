using System.Collections.Generic;
using System.Xml.Serialization;

namespace NugetSourceManager.Serialization
{
    [XmlRoot(ElementName = "apikeys")]
    public class Apikeys
    {
        [XmlElement(ElementName = "add")]
        public List<PackageSource> Add { get; set; }
    }
}