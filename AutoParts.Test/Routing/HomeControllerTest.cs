namespace AutoParts.Test.Routing
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
        public void IndexRouteShouldBBeMapped()
             => MyRouting
                         .Configuration()
         .ShouldMap("/")
         .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBBeMapped()
                => MyRouting
                            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error());
    }
}
