using System;
using FluentAssertions;
using NugetSourceManager.Serialization;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class RemoveTests : FeatureTests
    {
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
        public void RemoveSource_ShouldNotBreak_When_SourceDoesntExist()
        {
            _defaultSourceFile.RemovePackageSource(Guid.NewGuid().ToString());
        }

        [Fact]
        public void Removal_of_source_removes_entries_from_all_sections()
        {
            PackageSource packageSource = AddRandomSourceToDefault();

            //disable it 
            _defaultSourceFile.DisablePackageSource(packageSource.SourcePath);

            //now remove the source
            _defaultSourceFile.RemovePackageSource(packageSource.SourcePath);

            //now make sure it doesnt exist
            _defaultSourceFile.GetPackageSource(packageSource.Name)
                .Should().BeNull("package should be removed but still exists");

            //now make sure it doesn't exist in disabled entries
            _defaultSourceFile.XmlData.DisabledPackageSources
                .Contains(packageSource.Name)
                .Should().BeFalse("entry should have been removed from disabled section");
        }
    }
}