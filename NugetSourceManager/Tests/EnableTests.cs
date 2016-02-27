using System;
using FluentAssertions;
using Xunit;

namespace NugetSourceManager.Tests
{

    public class EnableTests :FeatureTests
    {
        [Fact]
        public void Can_Enable_Disabled_PackageSourceByPath()
        {
            var source = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(source.Name);

            _defaultSourceFile.EnablePackageSource(source.SourcePath);

            _defaultSourceFile.IsSourceDisabled(source.SourcePath)
                .Should().BeFalse("source should be enabled");
        }

        [Fact]
        public void Can_Enable_Disabled_PackageSourceByName()
        {
            var source = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(source.Name);

            _defaultSourceFile.EnablePackageSource(source.Name);

            _defaultSourceFile.IsSourceDisabled(source.Name)
                .Should().BeFalse("source should be enabled");
        }

        [Fact]
        public void Can_Enable_Enabled_PackageSourceByName()
        {
            var source = AddRandomSourceToDefault();
            _defaultSourceFile.EnablePackageSource(source.Name);
            _defaultSourceFile.IsSourceDisabled(source.Name)
                .Should().BeFalse("source should be enabled");
        }

        [Fact]
        public void Can_Enable_Invalid_PackageSourceByName()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _defaultSourceFile.EnablePackageSource(Guid.NewGuid().ToString());
            });
        }
    }
}