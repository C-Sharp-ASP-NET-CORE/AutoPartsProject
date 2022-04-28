namespace AutoParts.Test.Data
{
    using AutoParts.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Parts
    {
        public static IEnumerable<Part> TenPublicParts()
                => Enumerable.Range(0, 10).Select(i => new Part
                {
                    IsPublic = true
                });
    }
}
