using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class AddOrUpdateTests : FeatureTests
    {
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
    }
}