using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NugetSourceManager
{
    public class Tests
    {
        private readonly SourceFile _defaultSourceFile = DefaultSourceFile.GetInstace();

        [Fact]
        public void CanFindDefaultSourceFile()
        {
            string actualPath = _defaultSourceFile.Path;
            string expectedPath = @"C:\Users\bfazlani\AppData\Roaming\NuGet\NuGet.Config";
            string.Compare(actualPath,expectedPath, StringComparison.OrdinalIgnoreCase)
                .ShouldBeEquivalentTo(0);
        }

        [Fact]
        public void CanDeserializeDefaultSourceFile()
        {
            _defaultSourceFile.XmlData.Should().NotBeNull("xml is not deserialised");
        }

        [Fact]
        public void CanAddOrUpdate_When_SourceNameDoesNotExist()
        {
            string newSourceName = "NewSource";
            string newSourceValue = "http://nuget.newsourece.com/v2";

            _defaultSourceFile.AddOrUpdateSource(newSourceName, newSourceValue);

            PackageSource expectedRecord = _defaultSourceFile.XmlData.PackageSources.Entries.SingleOrDefault(
                x => x.Name == newSourceName && x.SourcePath == newSourceValue && x.ProtocolVersion == null);

            expectedRecord.Should().NotBeNull("did not find added nuget source");
        }

        private PackageSource AddRandomSourceToDefault(
            string name = null, string source = null)
        {
            string sourceName = name ?? Guid.NewGuid().ToString();
            string sourcePath = source ?? $"http://{Guid.NewGuid()}/org/nugets";

            _defaultSourceFile.AddOrUpdateSource(sourceName, sourcePath);

            return new PackageSource(sourceName, sourcePath);
        }

        [Fact]
        public void CanAddOrUpdate_When_SourceNameExist()
        {
            string packageName = Guid.NewGuid().ToString();
            string oldPackageSource = Guid.NewGuid().ToString();
            string newPackageSource = Guid.NewGuid().ToString();

            AddRandomSourceToDefault(packageName, oldPackageSource);

            AddRandomSourceToDefault(packageName, newPackageSource);

            //assert
            PackageSource expectedRecord = _defaultSourceFile.XmlData.PackageSources
                .Entries.SingleOrDefault(
                x => x.Name == packageName && 
                x.SourcePath == newPackageSource && 
                x.ProtocolVersion == null);

            expectedRecord.Should().NotBeNull("did not find added nuget source");

            PackageSource unExpectedRecord = _defaultSourceFile.XmlData.PackageSources
                .Entries.SingleOrDefault(
                x => x.Name == packageName &&
                x.SourcePath == oldPackageSource &&
                x.ProtocolVersion == null);

            unExpectedRecord.Should().BeNull("old record should not exist");
        }

        [Fact]
        public void Should_UpdateName_When_SourceExists()
        {
            string packageSource = Guid.NewGuid().ToString();
            string oldPackageName = Guid.NewGuid().ToString();
            string newPackagename = Guid.NewGuid().ToString();

            AddRandomSourceToDefault(oldPackageName, packageSource);

            AddRandomSourceToDefault(newPackagename, packageSource);

            //assert
            PackageSource expectedRecord = _defaultSourceFile.XmlData.PackageSources
                .Entries.SingleOrDefault(
                x => x.Name == newPackagename &&
                x.SourcePath == packageSource &&
                x.ProtocolVersion == null);

            expectedRecord.Should().NotBeNull("did not find added nuget source");

            PackageSource unExpectedRecord = _defaultSourceFile.XmlData.PackageSources
                .Entries.SingleOrDefault(
                x => x.Name == oldPackageName &&
                x.SourcePath == packageSource &&
                x.ProtocolVersion == null);

            unExpectedRecord.Should().BeNull("old record should not exist");
        }

        //multiple equals are when you provide a name that exists and
        //a source that exists but in different packages
        [Fact]
        public void Should_MergeSources_When_MultipleEqualfound()
        {
            string oldPackageSource = Guid.NewGuid().ToString();
            string newPackageSource = Guid.NewGuid().ToString();
            string oldPackageName = Guid.NewGuid().ToString();
            string newPackagename = Guid.NewGuid().ToString();

            AddRandomSourceToDefault(oldPackageName, oldPackageSource);
            AddRandomSourceToDefault(newPackagename, newPackageSource);

            AddRandomSourceToDefault(oldPackageName, newPackageSource);

            //assert
            _defaultSourceFile.XmlData.PackageSources
                .Entries
                .SingleOrDefault(x => x.Name == oldPackageName && x.SourcePath == oldPackageSource)
                .Should().BeNull("this package should not exist");

            _defaultSourceFile.XmlData.PackageSources
                .Entries
                .SingleOrDefault(x => x.Name == newPackageSource && x.SourcePath == newPackageSource)
                .Should().BeNull("this package should not exist");

            _defaultSourceFile.XmlData.PackageSources
                .Entries
                .SingleOrDefault(x => x.Name == oldPackageName && x.SourcePath == newPackageSource)
                .Should().NotBeNull("this package should exist!");
        }

        [Fact]
        public void Can_RemoveSource_WhenNameExists()
        {
            var packageSource = AddRandomSourceToDefault();

            //assert that it exists
            _defaultSourceFile.GetPackageSource(packageSource.Name)
                .ShouldBeEquivalentTo(packageSource);

            //now remove the source by name
            _defaultSourceFile.RemovePackageSource(packageSource.Name);

            //now make sure it doesnt exist
            _defaultSourceFile.GetPackageSource(packageSource.Name)
                .Should().BeNull("package should be removed but still exists");
        }

        [Fact]
        public void Can_RemoveSource_WhenSourcePathExists()
        {
            var packageSource = AddRandomSourceToDefault();

            //assert that it exists
            _defaultSourceFile.GetPackageSource(packageSource.SourcePath)
                .ShouldBeEquivalentTo(packageSource);

            //now remove the source by path
            _defaultSourceFile.RemovePackageSource(packageSource.SourcePath);

            //now make sure it doesnt exist
            _defaultSourceFile.GetPackageSource(packageSource.Name)
                .Should().BeNull("package should be removed but still exists");
        }

        [Fact]
        public void RemoveSource_ShouldNorBreak_When_SourceDoesntExist()
        {
            _defaultSourceFile.RemovePackageSource(Guid.NewGuid().ToString());
        }

        [Fact]
        public void Can_DisablePackageSource_WhenNameExists_And_NotAlreadyDisabled()
        {
            PackageSource source = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(source.Name);

            //assert
            _defaultSourceFile.IsSourceDisabled(source.Name)
                .Should().BeTrue("source should not be enabled");
        }

        [Fact]
        public void Can_DisablePackageSource_WhenNameExists_And_AlreadyDisabled()
        {
            PackageSource source = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(source.Name);

            _defaultSourceFile.IsSourceDisabled(source.Name)
                .Should().BeTrue("source should not be enabled");

            //now there is a source that is already disabled
            //lets disable again
            _defaultSourceFile.DisablePackageSource(source.Name);

            //assert
            _defaultSourceFile.IsSourceDisabled(source.Name)
                .Should().BeTrue("source should not be enabled");
        }

        [Fact]
        public void Can_DisablePackageSource_WhenPathExists_And_NotAlreadyDisabled()
        {
            PackageSource source = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(source.SourcePath);

            //assert
            _defaultSourceFile.IsSourceDisabled(source.SourcePath)
                .Should().BeTrue("source should not be enabled");
        }

        [Fact]
        public void Can_DisableSource_WhenSourceDoesntExist()
        {
            string randomName = Guid.NewGuid().ToString();

            //assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _defaultSourceFile.DisablePackageSource(randomName);
            });
        }
    }
}