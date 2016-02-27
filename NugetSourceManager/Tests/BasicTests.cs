using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class BasicTests
    {
        private readonly SourceFile _defaultSourceFile = DefaultSourceFile.GetInstace();

        private PackageSource AddRandomSourceToDefault(
            string name = null, string source = null)
        {
            string sourceName = name ?? Guid.NewGuid().ToString();
            string sourcePath = source ?? $"http://{Guid.NewGuid()}/org/nugets";

            _defaultSourceFile.AddOrUpdateSource(sourceName, sourcePath);

            return new PackageSource(sourceName, sourcePath);
        }

        [Fact]
        public void CanFindDefaultSourceFile()
        {
            string actualPath = _defaultSourceFile.Path;
            string expectedPath = @"C:\Users\bfazlani\AppData\Roaming\NuGet\NuGet.Config";
            string.Compare(actualPath, expectedPath, StringComparison.OrdinalIgnoreCase)
                .ShouldBeEquivalentTo(0);
        }

        [Fact]
        public void CanDeserializeDefaultSourceFile()
        {
            _defaultSourceFile.XmlData.Should().NotBeNull("xml is not deserialised");
        }
    }
}