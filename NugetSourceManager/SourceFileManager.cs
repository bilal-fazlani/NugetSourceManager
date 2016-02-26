using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NugetSourceManager
{
    internal class SourceFileManager
    {
        private readonly SourceFile _sourceFile;

        public SourceFileManager(SourceFile sourceFile)
        {
            this._sourceFile = sourceFile;
        }

        public void Save()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(typeof(XmlData));
            using (var filestream = new FileStream(_sourceFile.Path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(filestream, _sourceFile.XmlData, ns);
            }
        }
    }
}