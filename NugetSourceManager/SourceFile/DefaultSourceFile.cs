using System;

namespace NugetSourceManager.SourceFile
{
    public class DefaultSourceFile : SourceFileBase
    {
        private static readonly DefaultSourceFile Instance = new DefaultSourceFile();

        public static DefaultSourceFile GetInstace()
        {
            return Instance;
        }

        private DefaultSourceFile()
        {
            SetDefaultPath();
            LoadXml();
        }

        private void SetDefaultPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string nugetPath = System.IO.Path.Combine(appData, "Nuget", "Nuget.Config");
            this.Path = nugetPath;
        }
    }
}