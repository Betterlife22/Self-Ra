using Selfra_Core.Base;

namespace Selfra_Entity.Model
{
    public class NewsCategory:BaseEntity
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
    }
}
