namespace AutoParts.Core.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalParts { get; init; }
        public int TotalUsers { get; init; }
        public List<PartIndexViewModel> Parts { get; init; }
    }
}
