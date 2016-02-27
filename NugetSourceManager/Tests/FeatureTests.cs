using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class FeatureTests
    {
        protected readonly SourceFile _defaultSourceFile = DefaultSourceFile.GetInstace();

        protected PackageSource AddRandomSourceToDefault(
            string name = null, string source = null)
        {
            string sourceName = name ?? Guid.NewGuid().ToString();
            string sourcePath = source ?? $"http://{Guid.NewGuid()}/org/nugets";

            _defaultSourceFile.AddOrUpdateSource(sourceName, sourcePath);

            return new PackageSource(sourceName, sourcePath);
        }
    }
}