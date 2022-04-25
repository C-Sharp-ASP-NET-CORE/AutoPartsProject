namespace AutoParts.Core.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AutoParts.Core.Constants;
    using AutoParts.Core.Contract;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly AutoPartsDbContext data;
        private readonly IConfigurationProvider mapper;

        public PartService(
            AutoPartsDbContext data,
            IMapper mapper)
        { 
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public PartQueryServiceModel All(
            string brand,
            string searchTerm,
            PartsSorting sorting,
            int currentPage,
            int partsPerPage
            )
        {
            var partsQuery = this.data.Parts.Where(p => p.IsPublic);

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
                DealerId = dealerId,
                IsPublic = false
            };

            this.data.Parts.Add(myPart);
            this.data.SaveChanges();

            return myPart.Id;
        }

        public PartDetailsServiceModel Details(int id)
                     => this.data.Parts
                                    .Where(p => p.Id == id)
                                    .ProjectTo<PartDetailsServiceModel>(this.mapper)
                                    .FirstOrDefault();

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
            myPart.IsPublic = false;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<LatestPartServiceModel> Latest()
                    => this.data
                            .Parts
                               .Where(p=>p.IsPublic)
                               .OrderByDescending(c => c.Id)
                               .ProjectTo<LatestPartServiceModel>(this.mapper)
                               .Take(3)
                               .ToList();

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
