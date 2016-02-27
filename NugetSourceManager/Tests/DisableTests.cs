using System;
using FluentAssertions;
using NugetSourceManager.Serialization;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class DisableTests : FeatureTests
    {
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