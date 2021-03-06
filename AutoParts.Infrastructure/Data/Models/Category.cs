namespace AutoParts.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    public class Category
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Part> Parts { get; set; } = new List<Part>();
    }
}
