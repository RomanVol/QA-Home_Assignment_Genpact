using Newtonsoft.Json.Linq;
using WikipediaPlaywrightTests.Config;
using WikipediaPlaywrightTests.Utils;
using System.Text.Json;
using HtmlAgilityPack;

namespace WikipediaPlaywrightTests.Services
{
    public class MediaWikiApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        
        public MediaWikiApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "WikipediaPlaywrightTests/1.0 (Educational Testing Purpose)");
            _apiUrl = TestConfiguration.MediaWikiApiUrl;
        }
        
        /// <summary>
        /// Extracts a specific section from a Wikipedia page using MediaWiki Parse API
        /// </summary>
        public async Task<string> GetSectionText(string pageTitle, string sectionTitle)
        {
            try
            {
                var sectionNumber = await FindSectionNumberByTitle(pageTitle, sectionTitle);
                var html = await FetchSectionHtml(pageTitle, sectionNumber);
                var extractedText = ExtractTextFromHtml(html);
                var cleanedText = CleanAndDecodeText(extractedText);
                var normalizedText = TextNormalizer.Normalize(cleanedText);
                
                return normalizedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching section from MediaWiki API: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Finds the section number by searching for a section title
        /// </summary>
        private async Task<int> FindSectionNumberByTitle(string pageTitle, string sectionTitle)
        {
            var url = $"{_apiUrl}?action=parse&page={Uri.EscapeDataString(pageTitle)}&format=json&prop=sections|text";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            
            var sections = json["parse"]?["sections"];
            if (sections == null)
                throw new Exception("No sections found in the response");
            
            foreach (var section in sections)
            {
                var line = section["line"]?.ToString();
                if (line != null && line.Contains(sectionTitle, StringComparison.OrdinalIgnoreCase))
                {
                    var sectionNumber = section["index"]?.Value<int>();
                    if (sectionNumber.HasValue)
                        return sectionNumber.Value;
                }
            }
            
            throw new Exception($"Section '{sectionTitle}' not found");
        }
        
        /// <summary>
        /// Fetches the HTML content for a specific section
        /// </summary>
        private async Task<string> FetchSectionHtml(string pageTitle, int sectionNumber)
        {
            var sectionUrl = $"{_apiUrl}?action=parse&page={Uri.EscapeDataString(pageTitle)}&format=json&prop=text&section={sectionNumber}";
            
            var sectionResponse = await _httpClient.GetStringAsync(sectionUrl);
            
            using var docJson = JsonDocument.Parse(sectionResponse);
            var html = docJson.RootElement
                .GetProperty("parse")
                .GetProperty("text")
                .GetProperty("*")
                .GetString();

            if (html == null)
                throw new Exception("Failed to extract section text");
            
            return html;
        }
        
        /// <summary>
        /// Extracts text from HTML using HtmlAgilityPack
        /// </summary>
        private string ExtractTextFromHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var debugDiv = doc.DocumentNode
                .SelectSingleNode("//h3[@id='Debugging_features']/ancestor::div[1]");

            if (debugDiv == null)
                throw new Exception("Debugging_features section not found in HTML.");

            var extractedText = "";
            
            extractedText += ExtractParagraphText(debugDiv);
            extractedText += ExtractListItemsText(debugDiv);
            
            return extractedText;
        }
        
        /// <summary>
        /// Extracts text from the first paragraph element
        /// </summary>
        private string ExtractParagraphText(HtmlNode parentNode)
        {
            var p = parentNode.SelectSingleNode("following::p[1]");
            if (p == null)
                return string.Empty;
            
            string pText = string.Join(" ",
                p.ChildNodes
                    .Where(n => n.NodeType == HtmlNodeType.Text)
                    .Select(n => n.InnerText.Trim())
                    .Where(t => !string.IsNullOrEmpty(t))
            );
            
            return pText + " ";
        }
        
        /// <summary>
        /// Extracts text from all list items
        /// </summary>
        private string ExtractListItemsText(HtmlNode parentNode)
        {
            var listItems = parentNode.SelectNodes("following::ul[1]/li");
            if (listItems == null)
                return string.Empty;
            
            var extractedText = "";
            for (int i = 0; i < listItems.Count; i++)
            {
                var li = listItems[i];
                string liText = string.Join(" ",
                    li.ChildNodes
                        .Where(n => n.NodeType == HtmlNodeType.Text)
                        .Select(n => n.InnerText.Trim())
                        .Where(t => !string.IsNullOrEmpty(t))
                );
                
                extractedText += liText + " ";
            }
            
            return extractedText;
        }
        
        /// <summary>
        /// Cleans and decodes extracted text
        /// </summary>
        private string CleanAndDecodeText(string text)
        {
            var plainText = System.Net.WebUtility.HtmlDecode(text.Trim());
            
            // Remove reference markers like [1], [2], etc. and collapse whitespace
            plainText = System.Text.RegularExpressions.Regex.Replace(plainText, @"\[\d+\]", " ");
            plainText = System.Text.RegularExpressions.Regex.Replace(plainText, @"\s+", " ").Trim();
            
            return plainText;
        }
        
        /// <summary>
        /// Gets all sections from a Wikipedia page
        /// </summary>
        public async Task<List<(string Title, int Index)>> GetAllSections(string pageTitle)
        {
            try
            {
                var url = $"{_apiUrl}?action=parse&page={Uri.EscapeDataString(pageTitle)}&format=json&prop=sections";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);
                
                var sections = json["parse"]?["sections"];
                var result = new List<(string Title, int Index)>();
                
                if (sections != null)
                {
                    foreach (var section in sections)
                    {
                        var title = section["line"]?.ToString() ?? string.Empty;
                        var index = section["index"]?.Value<int>() ?? 0;
                        result.Add((title, index));
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching sections from MediaWiki API: {ex.Message}", ex);
            }
        }
    }
}
