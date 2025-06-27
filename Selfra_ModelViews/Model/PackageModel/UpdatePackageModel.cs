
namespace Selfra_ModelViews.Model.PackageModel
{
    public class UpdatePackageModel
    {
        public string? PackageId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public decimal Price { get; set; }
    }
}
