using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class Package:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public decimal Price { get; set; }
    }
}
