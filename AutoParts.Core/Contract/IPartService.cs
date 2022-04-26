namespace AutoParts.Core.Contract
{
    using AutoParts.Core.Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        PartQueryServiceModel All(
            string brand=null,
            string searchTerm=null,
            PartsSorting sorting= PartsSorting.DateCreated,
            int currentPage = 1,
            int partsPerPage = int.MaxValue,
            bool publicOnly = true);

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
               bool isPublic);

        public PartDetailsServiceModel Details(int id);
        IEnumerable<LatestPartServiceModel> Latest();
        IEnumerable<PartServiceModel> ByUser(string userId);
        bool IsByDealer(int carId, int dealerId);
        void ChangeVisibility(int parId);
        IEnumerable<string> AllPartBrands();
        IEnumerable<PartCategoryServiceModel> AllPartCategories();

        bool CategoryExists(int categoryId);
    }
}
