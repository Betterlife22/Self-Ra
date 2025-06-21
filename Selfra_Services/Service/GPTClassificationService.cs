using HtmlAgilityPack;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class GPTClassificationService : IGPTClassificationService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _openAI;
        public GPTClassificationService(HttpClient httpClient, OpenAISettings openAISettings)
        {
            _httpClient = httpClient;
            _openAI = openAISettings;
        }
        public async Task<string> ClassifyCategoryAsync(string input)
        {
            var text = input.ToLowerInvariant();

            if (ContainsAny(text, "bóng đá", "thể thao", "giải đấu", "vòng loại", "cầu thủ", "trận đấu"))
                return "Thể thao";

            if (ContainsAny(text, "bệnh", "sức khỏe", "dịch bệnh", "dinh dưỡng", "bác sĩ", "tiêm chủng", "điều trị"))
                return "Sức khỏe";

            if (ContainsAny(text, "phim", "diễn viên", "showbiz", "ca sĩ", "giải trí", "truyền hình"))
                return "Giải trí";

            if (ContainsAny(text, "học sinh", "giáo viên", "giáo dục", "đại học", "thi tốt nghiệp", "bài giảng"))
                return "Giáo dục";

            if (ContainsAny(text, "tòa án", "phạm tội", "bắt giữ", "pháp luật", "cảnh sát", "xét xử", "khởi tố"))
                return "Pháp luật";

            if (ContainsAny(text, "chính phủ", "bộ trưởng", "quốc hội", "thời sự", "chính trị", "kinh tế", "tin tức"))
                return "Thời sự";

            return "Khác";
        }

        public async Task<string> ClassifyCategoryFromUrlAsync(string articleUrl)
        {
            var content = await ExtractContentFromUrlAsync(articleUrl);
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("⚠️ Không lấy được nội dung từ URL.");
                return "Khác";
            }

            return await ClassifyCategoryAsync(content);
        }

        private async Task<string> ExtractContentFromUrlAsync(string articleUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(articleUrl);
                if (!response.IsSuccessStatusCode)
                    return "";

                var html = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var contentNode = doc.DocumentNode.SelectSingleNode("//article[contains(@class,'fck_detail')]");
                if (contentNode == null)
                    return "";

                var paragraphs = contentNode.SelectNodes(".//p");
                var fullText = new StringBuilder();

                if (paragraphs != null)
                {
                    foreach (var p in paragraphs)
                    {
                        var text = p.InnerText?.Trim();
                        if (!string.IsNullOrEmpty(text))
                            fullText.AppendLine(text);
                    }
                }

                return fullText.ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi tải bài viết: {ex.Message}");
                return "";
            }
        }

        private bool ContainsAny(string text, params string[] keywords)
        {
            return keywords.Any(k => text.Contains(k));
        }
    }
}

