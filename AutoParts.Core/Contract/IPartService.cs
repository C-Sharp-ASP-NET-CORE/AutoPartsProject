namespace AutoParts.Core.Contract
{
    using AutoParts.Core.Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        PartQueryServiceModel All(
            string brand,
            string searchTerm,
            PartsSorting sorting,
            int currentPage,
            int partsPerPage
            );

        int Create(
                int categoryId,
                string manufacturer,
                string carBrand,
                string carModel,
                decimal price,
                string description,
                string serialNumber,
                string imageUrl,
                int year,
                bool isUsed,
                int dealerId);

        bool Edit(
               int id,
               int categoryId,
               string manufacturer,
               string carBrand,
               string carModel,
               decimal price,
               string description,
               string serialNumber,
               string imageUrl,
               int year,
               bool isUsed,
               int dealerId);

        public PartDetailsServiceModel Details(int id);
        IEnumerable<LatestPartServiceModel> Latest();
        IEnumerable<PartServiceModel> ByUser(string userId);
        IEnumerable<string> AllPartBrands();
        IEnumerable<PartCategoryServiceModel> AllPartCategories();

        bool CategoryExists(int categoryId);
    }
}
