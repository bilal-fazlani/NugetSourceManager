using System;
using System.Linq;
using FluentAssertions;
using NugetSourceManager.Serialization;
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

        [Fact]
        public void Can_UpdateName_InDisabledSection_when_updating_singleRecord()
        {
            string oldName = Guid.NewGuid().ToString();
            string path = Guid.NewGuid().ToString();
            string newName = Guid.NewGuid().ToString();

            //add source
            _defaultSourceFile.AddOrUpdateSource(oldName, path);

            //disable it
            _defaultSourceFile.DisablePackageSource(oldName);

            //update its name
            _defaultSourceFile.AddOrUpdateSource(newName, path);

            //old should not exist in disabled entires
            _defaultSourceFile.XmlData.DisabledPackageSources.Entries
                .Any(x => string.Compare(x.Name, oldName, StringComparison.OrdinalIgnoreCase) == 0)
                .Should().BeFalse();

            //new should exist in disable entries
            _defaultSourceFile.XmlData.DisabledPackageSources.Entries
                .Any(x => string.Compare(x.Name, newName, StringComparison.OrdinalIgnoreCase) == 0)
                .Should().BeTrue();
        }


        [Fact]
        public void Can_UpdateName_InDisabledSection_when_updating_multiple_records()
        {
            string name_a = Guid.NewGuid().ToString();
            string path_a = Guid.NewGuid().ToString();
            string name_b = Guid.NewGuid().ToString();
            string path_b = Guid.NewGuid().ToString();

            //add source
            _defaultSourceFile.AddOrUpdateSource(name_a, path_a);

            //disable it
            _defaultSourceFile.DisablePackageSource(name_a);

            //add another source
            _defaultSourceFile.AddOrUpdateSource(name_b, path_b);

            //now merge 2 sources to reflect name a but path b
            _defaultSourceFile.AddOrUpdateSource(name_a, path_b);

            //name_b should not exist in disabled entries
            _defaultSourceFile.XmlData.DisabledPackageSources.Entries
                .Any(x => string.Compare(x.Name, name_b, StringComparison.OrdinalIgnoreCase) == 0)
                .Should().BeFalse();

            //name_a should exist in disable entries
            _defaultSourceFile.XmlData.DisabledPackageSources.Entries
                .Any(x => string.Compare(x.Name, name_a, StringComparison.OrdinalIgnoreCase) == 0)
                .Should().BeTrue();
        }
    }
}