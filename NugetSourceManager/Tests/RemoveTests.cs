using System;
using FluentAssertions;
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
        public void RemoveSource_ShouldNorBreak_When_SourceDoesntExist()
        {
            _defaultSourceFile.RemovePackageSource(Guid.NewGuid().ToString());
        }
    }
}