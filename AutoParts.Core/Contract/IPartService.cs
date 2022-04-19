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

        IEnumerable<PartServiceModel> ByUser(string userId);
        IEnumerable<string> AllPartBrands();
    }
}
