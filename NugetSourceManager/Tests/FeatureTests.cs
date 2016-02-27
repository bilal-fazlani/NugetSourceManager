using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NugetSourceManager.Serialization;
using NugetSourceManager.Serialization.Entries;
using NugetSourceManager.SourceFile;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class FeatureTests
    {
        protected readonly SourceFileBase _defaultSourceFile = DefaultSourceFile.GetInstace();

        protected PackageSourceEntry AddRandomSourceToDefault(
            string name = null, string source = null)
        {
            string sourceName = name ?? Guid.NewGuid().ToString();
            string sourcePath = source ?? $"http://{Guid.NewGuid()}/org/nugets";

            _defaultSourceFile.AddOrUpdateSource(sourceName, sourcePath);

            return new PackageSourceEntry(sourceName, sourcePath);
        }
    }
}