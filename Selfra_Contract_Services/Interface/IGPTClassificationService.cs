

namespace Selfra_Contract_Services.Interface
{
    public interface IGPTClassificationService
    {
        public Task<string> ClassifyCategoryAsync(string input);
        public Task<string> ClassifyCategoryFromUrlAsync(string articleUrl);
    }
}
