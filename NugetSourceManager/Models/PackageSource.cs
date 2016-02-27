using System.Text;
using NugetSourceManager.Serialization.Entries;
using NugetSourceManager.SourceFile;

namespace NugetSourceManager.Models
{
    public class PackageSourceModel
    {
        public string Name { get; set; }

        public string SourcePath { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string activeIndicator = IsActive ? "→ " : "  ";

            sb.Append(activeIndicator);
            sb.AppendLine(Name);
            sb.Append("  ");
            sb.AppendLine(SourcePath);
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
