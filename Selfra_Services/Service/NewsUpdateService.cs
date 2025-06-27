using Microsoft.Extensions.DependencyInjection;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selft.Contract.Repositories.Interface;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace Selfra_Services.Service
{
    public class NewsUpdateService : INewsUpdateService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;

        public NewsUpdateService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory, HttpClient httpClient, IUnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateNewsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var gptService = scope.ServiceProvider.GetRequiredService<GPTClassificationService>();
            var articles = await ReadFromVnExpressAsync();

            var newPost = new List<Post>();

            foreach (var item in articles)
            {
                string articleUrl = "";
                string imageUrl = "";
                string description = "";

                if (!string.IsNullOrEmpty(item.Description))
                {
                    // Extract article link (href="...")
                    var hrefMatch = Regex.Match(item.Description, "href\\s*=\\s*\"([^\"]+)\"");
                    if (hrefMatch.Success)
                        articleUrl = hrefMatch.Groups[1].Value;

                    // Extract image link (src="...")
                    var srcMatch = Regex.Match(item.Description, "src\\s*=\\s*\"([^\"]+)\"");
                    if (srcMatch.Success)
                        imageUrl = srcMatch.Groups[1].Value;

                    // Extract description text after </br>
                    var brTag = "</br>";
                    var brIndex = item.Description.IndexOf(brTag, StringComparison.OrdinalIgnoreCase);
                    if (brIndex != -1 && brIndex + brTag.Length < item.Description.Length)
                        description = item.Description.Substring(brIndex + brTag.Length).Trim();
                }

                var fullText = $"{item.Title}\n{description}";
                var category = await gptService.ClassifyCategoryFromUrlAsync(articleUrl);

                newPost.Add(new Post
                {
                    Title = item.Title,
                    ArticleUrl = articleUrl,
                    ImageUrl = imageUrl,
                    Content = description,
                    CategoryPost = category,
                    IsActive = true,
                });
            }

            foreach (var item in newPost)
            {
                await _unitOfWork.GetRepository<Post>().AddAsync(item);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<RssArticle>> ReadFromVnExpressAsync()
        {
            var feedUrl = "https://vnexpress.net/rss/tin-moi-nhat.rss";
            var articles = new List<RssArticle>();

            using var response = await _httpClient.GetAsync(feedUrl);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var xmlReader = XmlReader.Create(stream);
            var feed = SyndicationFeed.Load(xmlReader);

            if (feed?.Items != null)
            {
                foreach (var item in feed.Items)
                {
                    articles.Add(new RssArticle
                    {
                        Title = item.Title?.Text,
                        Description = item.Summary?.Text,
                        PublishedAt = item.PublishDate.UtcDateTime
                    });
                }
            }

            return articles;
        }
    }

    public class RssArticle
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
