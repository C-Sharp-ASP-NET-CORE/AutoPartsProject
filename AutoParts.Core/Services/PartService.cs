namespace AutoParts.Core.Services
{
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PartService:IPartService
    {
        private readonly AutoPartsDbContext data;

        public PartService(AutoPartsDbContext data)
        =>this.data = data;

        public PartQueryServiceModel All(
            string brand,
            string searchTerm,
            PartsSorting sorting,
            int currentPage,
            int partsPerPage
            )
        {
            var partsQuery = this.data.Parts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                partsQuery = partsQuery.Where(p => p.CarBrand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                partsQuery = partsQuery
                                    .Where(p =>
                                    p.Category.Name.ToLower().Contains(searchTerm.ToLower())
                                    || (p.CarBrand + " " + p.CarModel).ToLower().Contains(searchTerm.ToLower()));
            }

            var totalParts = partsQuery.Count();

            partsQuery = sorting switch
            {
                PartsSorting.Year => partsQuery.OrderByDescending(p => p.Year),
                PartsSorting.Price => partsQuery.OrderByDescending(p => p.Price),
                PartsSorting.BrandAndMode => partsQuery.OrderBy(p => p.CarBrand).ThenBy(p => p.CarModel),
                PartsSorting.DateCreated or _ => partsQuery.OrderByDescending(p => p.Id)
            };

            var parts = GetParts(partsQuery
                                .Skip((currentPage - 1) * partsPerPage)
                                .Take(partsPerPage));

            return new PartQueryServiceModel
            { 
                TotalParts= totalParts,
                CurrentPage= currentPage,
                PartsPerPage= partsPerPage,
                Parts = parts
            };
        }

        public IEnumerable<string> AllPartBrands()
        => this.data.Parts
                 .Select(c => c.CarBrand)
                 .Distinct()
                 .OrderBy(br => br)
                 .ToList();

        public IEnumerable<PartServiceModel> ByUser(string userId)
              => this.GetParts(this.data
                                .Parts
                                .Where(p => p.Dealer.UserId == userId));

        private IEnumerable<PartServiceModel> GetParts(IQueryable<Part> partQuery)
                     => partQuery
                                .Select(p => new PartServiceModel
                                {
                                    Id = p.Id,
                                    Category = p.Category.Name,
                                    CarBrand = p.CarBrand,
                                    CarModel = p.CarModel,
                                    Price = p.Price,
                                    Year = p.Year,
                                    ImageUrl = p.ImageUrl
                                })
                                .ToList();
    }
}
