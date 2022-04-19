namespace AutoParts.Core.Models.Parts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PartQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int PartsPerPage { get; init; }

        public int TotalParts { get; init; }

        public IEnumerable<PartServiceModel> Parts { get; init; }
    }
}