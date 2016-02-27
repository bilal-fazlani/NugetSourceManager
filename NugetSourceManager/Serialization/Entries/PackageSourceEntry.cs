using System;
using System.Xml.Serialization;

namespace NugetSourceManager.Serialization.Entries
{
    [XmlRoot(ElementName = "add")]
    public class PackageSourceEntry
    {
        public PackageSourceEntry()
        {

        }

        public PackageSourceEntry(string name, string sourcePath, string protocolVersion = null)
        {
            Name = name;
            SourcePath = sourcePath;
            ProtocolVersion = protocolVersion;
        }

        [XmlAttribute(AttributeName = "key")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string SourcePath { get; set; }

        [XmlAttribute(AttributeName = "protocolVersion")]
        public string ProtocolVersion { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj as PackageSourceEntry) != null)
            {
                PackageSourceEntry otherSource = (PackageSourceEntry)obj;

                bool nameEquals = string.Compare(otherSource.Name, Name, StringComparison.OrdinalIgnoreCase) == 0;

                bool urlEquals = string.Compare(this.SourcePath, otherSource.SourcePath,
                    StringComparison.OrdinalIgnoreCase) == 0;

                return nameEquals | urlEquals;
            }

            if ((obj as string) != null)
            {
                string nameOrSource = (string)obj;
                return string.Compare(Name, nameOrSource, StringComparison.OrdinalIgnoreCase) == 0 ||
                       string.Compare(SourcePath, nameOrSource, StringComparison.OrdinalIgnoreCase) == 0;
            }

            return false;
        }
    }
}