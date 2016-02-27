using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using NugetSourceManager.Serialization.Entries;

namespace NugetSourceManager.Serialization.Sections
{
    [XmlRoot(ElementName = "disabledPackageSources")]
    public class DisabledPackageSources
    {
        [XmlElement(ElementName = "add")]
        public List<DisabledSourceEntry> Entries { get; set; } = new List<DisabledSourceEntry>();

        public bool Contains(string name)
        {
            return Entries.Any(x => 
                string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void Add(string name)
        {
            if (!Contains(name))
            {
                Entries.Add(new DisabledSourceEntry
                {
                    Name = name
                });
            }
            else
            {
                Entries
                    .Single(x =>
                        string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0)
                    .Disabled = true;
            }
        }

        public void Remove(string name)
        {
            if (Contains(name))
            {
                var entry = Entries
                    .Single(x =>
                        string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
                Entries.Remove(entry);
            }
        }
    }
}