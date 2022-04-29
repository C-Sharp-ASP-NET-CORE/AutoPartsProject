namespace AutoParts.Core.Models.Parts
{
    using System.Collections.Generic;

    public class PartQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int PartsPerPage { get; init; }

        public int TotalParts { get; init; }

        public IEnumerable<PartServiceModel> Parts { get; init; }
    }
}