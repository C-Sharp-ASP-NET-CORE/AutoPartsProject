namespace AutoParts.Core.Models.Parts
{
    using AutoParts.Core.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PartServiceModel:IPartModel
    {
        public int Id { get; init; }

        public string CarBrand { get; init; }

        public string CarModel { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
        public decimal Price { get; init; }

        public string Category { get; init; }
    }
}