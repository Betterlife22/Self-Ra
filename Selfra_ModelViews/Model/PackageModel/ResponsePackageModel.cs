
using System.ComponentModel.DataAnnotations;

namespace Selfra_ModelViews.Model.PackageModel
{
    public class ResponsePackageModel
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public decimal Price { get; set; }
        public int Rank { get; set; }
    }
}
