using System;
using System.IO;
using FluentAssertions;
using NugetSourceManager.Serialization;
using NugetSourceManager.Serialization.Entries;
using NugetSourceManager.SourceFile;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class BasicTests
    {
        private readonly SourceFileBase _defaultSourceFile = DefaultSourceFile.GetInstace();

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