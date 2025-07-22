

namespace Selfra_ModelViews.Model.PackageModel
{
    public class CreatePackageModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public bool IsPublic { get; set; }
        public decimal Price { get; set; }
        public int Rank { get; set; }
    }
}
