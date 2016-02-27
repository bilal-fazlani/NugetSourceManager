using System;
using FluentAssertions;
using NugetSourceManager.SourceFile;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class LoadXmlTests : SourceFileBase
    {
        public LoadXmlTests()
        {
            //Random folder and random file
            this.Path = System.IO.Path.Combine(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                "Nuget.Config");

            LoadXml();
        }

        [Fact]
        public void Can_CreateFile_WhenFileDoesntExist()
        {
            XmlData.Should().NotBeNull("xmldata should not be null");
        }
    }
}