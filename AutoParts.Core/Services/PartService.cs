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

    public class PartService : IPartService
    {
        private readonly AutoPartsDbContext data;

        public PartService(AutoPartsDbContext data)
        => this.data = data;

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
                TotalParts = totalParts,
                CurrentPage = currentPage,
                PartsPerPage = partsPerPage,
                Parts = parts
            };
        }

        public IEnumerable<string> AllPartBrands()
        => this.data.Parts
                 .Select(c => c.CarBrand)
                 .Distinct()
                 .OrderBy(br => br)
                 .ToList();

        public IEnumerable<PartCategoryServiceModel> AllPartCategories()
                     => this.data.Categories
                         .Select(c => new PartCategoryServiceModel
                         {
                             Id = c.Id,
                             Name = c.Name
                         })
                         .ToList();

        public IEnumerable<PartServiceModel> ByUser(string userId)
                     => this.GetParts(this.data
                                .Parts
                                .Where(p => p.Dealer.UserId == userId));

        public bool CategoryExists(int categoryId)
                     => this.data.Categories
                                .Any(c => c.Id == categoryId);

        public int Create(
                    int categoryId, string manufacturer,
                    string carBrand, string carModel,
                    decimal price, string description,
                    string serialNumber, string imageUrl,
                    int year, bool isUsed, int dealerId)
        {

            var myPart = new Part
            {
                CategoryId = categoryId,
                Manufacturer = manufacturer,
                CarBrand = carBrand,
                CarModel = carModel,
                Price = price,
                Description = description,
                SerialNumber = serialNumber,
                ImageUrl = imageUrl,
                Year = year,
                IsUsed = isUsed,
                DealerId = dealerId
            };

            this.data.Parts.Add(myPart);
            this.data.SaveChanges();

            return myPart.Id;
        }

        public PartDetailsServiceModel Details(int id)
                     => this.data.Parts
                                    .Where(p => p.Id == id)
                                    .Select(p => new PartDetailsServiceModel
                                    {
                                        Id = p.Id,
                                        CategoryName = p.Category.Name,
                                        CarBrand = p.CarBrand,
                                        CarModel = p.CarModel,
                                        Price = p.Price,
                                        Year = p.Year,
                                        Description = p.Description,
                                        ImageUrl = p.ImageUrl,
                                        DealerId = p.DealerId,
                                        DealerName = p.Dealer.Name,
                                        UserId = p.Dealer.UserId
                                    }).FirstOrDefault();

        public bool Edit(
                    int id, int categoryId, string manufacturer,
                    string carBrand, string carModel,
                    decimal price, string description,
                    string serialNumber, string imageUrl,
                    int year, bool isUsed, int dealerId)
        {
            var myPart = this.data.Parts.Find(id);

            if (myPart.DealerId != dealerId)
            {
                return false;
            }

            myPart.CategoryId = categoryId;
            myPart.Manufacturer = manufacturer;
            myPart.CarBrand = carBrand;
            myPart.CarModel = carModel;
            myPart.Price = price;
            myPart.Description = description;
            myPart.SerialNumber = serialNumber;
            myPart.ImageUrl = imageUrl;
            myPart.Year = year;
            myPart.IsUsed = isUsed;

            this.data.SaveChanges();

            return true;
        }

        private IEnumerable<PartServiceModel> GetParts(IQueryable<Part> partQuery)
                     => partQuery
                                .Select(p => new PartServiceModel
                                {
                                    Id = p.Id,
                                    CategoryName = p.Category.Name,
                                    CarBrand = p.CarBrand,
                                    CarModel = p.CarModel,
                                    Price = p.Price,
                                    Year = p.Year,
                                    ImageUrl = p.ImageUrl
                                })
                                .ToList();
    }
}
