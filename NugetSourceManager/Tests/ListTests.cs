using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NugetSourceManager.Models;
using Xunit;

namespace NugetSourceManager.Tests
{
    public class ListTests : FeatureTests
    {
        [Fact]
        public void CanListPackages()
        {
            var enabledSource = AddRandomSourceToDefault();
            var disabledSource = AddRandomSourceToDefault();

            _defaultSourceFile.DisablePackageSource(disabledSource.Name);

            List<PackageSourceModel> sources = _defaultSourceFile.List();

            sources.Should()
                .Contain(x => x.Name == enabledSource.Name && 
                x.SourcePath == enabledSource.SourcePath &&
                x.IsActive);

            sources.Should()
                .Contain(x => x.Name == disabledSource.Name &&
                x.SourcePath == disabledSource.SourcePath &&
                !x.IsActive);
        }
    }
}
