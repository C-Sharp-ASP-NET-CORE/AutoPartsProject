namespace AutoParts.Test.Controller
{
    using AutoParts.Controllers;
    using AutoParts.Core.Models.Parts;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Xunit;
    using static Data.Parts;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexActionShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                        .Instance(instance => instance
                            .WithData(TenPublicParts()))
                        .Calling(c => c.Index())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry=>entry
                                .WithKey(LatestPartsCacheKey)
                                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                                .WithValueOfType<List<LatestPartServiceModel>>()))
                        .AndAlso()
                        .ShouldReturn()
                        .View(view => view
                            .WithModelOfType<List<LatestPartServiceModel>>()
                            .Passing(model => model.Should().HaveCount(0)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
