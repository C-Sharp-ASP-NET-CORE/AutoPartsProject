namespace AutoParts.Core.Models.Parts
{
    using AutoParts.Core.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LatestPartServiceModel : IPartModel
    {
        public int Id { get; init; }

        public string CarBrand { get; init; }

        public string CarModel { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}