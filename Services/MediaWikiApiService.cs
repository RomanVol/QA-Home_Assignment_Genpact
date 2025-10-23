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
                // First, get the parsed content
                var url = $"{_apiUrl}?action=parse&page={Uri.EscapeDataString(pageTitle)}&format=json&prop=sections|text";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);
                
                // Find section number for "Debugging features"
                var sections = json["parse"]?["sections"];
                if (sections == null)
                    throw new Exception("No sections found in the response");
                
                int? sectionNumber = null;
                foreach (var section in sections)
                {
                    var line = section["line"]?.ToString();
                    if (line != null && line.Contains(sectionTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        // Console.WriteLine($"[DEBUG] line = section: {line}");
                        sectionNumber = section["index"]?.Value<int>();
                        break;
                    }
                }
                
                if (sectionNumber == null)
                    throw new Exception($"Section '{sectionTitle}' not found");
                
                // Get the specific section content
                var sectionUrl = $"{_apiUrl}?action=parse&page={Uri.EscapeDataString(pageTitle)}&format=json&prop=text&section={sectionNumber}";
                Console.WriteLine($"[DEBUG] API URL: {sectionUrl}");
                
                var sectionResponse = await _httpClient.GetStringAsync(sectionUrl);
                
                // Extract the HTML from the JSON using System.Text.Json
                using var docJson = JsonDocument.Parse(sectionResponse);
                var html = docJson.RootElement
                    .GetProperty("parse")
                    .GetProperty("text")
                    .GetProperty("*")
                    .GetString();

                if (html == null)
                    throw new Exception("Failed to extract section text");
                
                // Parse the HTML with HtmlAgilityPack
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Find the parent div of <h3 id="Debugging_features">
                var debugDiv = doc.DocumentNode
                    .SelectSingleNode("//h3[@id='Debugging_features']/ancestor::div[1]");

                if (debugDiv == null)
                    throw new Exception("Debugging_features section not found in HTML.");

                var extractedText = "";
                
                Console.WriteLine("[DEBUG] ===== EXTRACTING TEXT FROM <p> ELEMENT =====");
                // Find the first <p> after that div
                var p = debugDiv.SelectSingleNode("following::p[1]");
                if (p != null)
                {
                    string pText = string.Join(" ",
                        p.ChildNodes
                            .Where(n => n.NodeType == HtmlNodeType.Text)
                            .Select(n => n.InnerText.Trim())
                            .Where(t => !string.IsNullOrEmpty(t))
                    );
                    Console.WriteLine($"[DEBUG] P TEXT: {pText}");
                    extractedText += pText + " ";
                }
                
                Console.WriteLine("[DEBUG] ===== EXTRACTING TEXT FROM <li> ELEMENTS =====");
                // Find all <li> elements below that div (inside a <ul>)
                var listItems = debugDiv.SelectNodes("following::ul[1]/li");
                if (listItems != null)
                {
                    // Include all <li> elements
                    for (int i = 0; i < listItems.Count; i++)
                    {
                        var li = listItems[i];
                        string liText = string.Join(" ",
                            li.ChildNodes
                                .Where(n => n.NodeType == HtmlNodeType.Text)
                                .Select(n => n.InnerText.Trim())
                                .Where(t => !string.IsNullOrEmpty(t))
                        );
                        Console.WriteLine($"[DEBUG] <li> text [{i+1}]: {liText}");
                        extractedText += liText + " ";
                    }
                    Console.WriteLine($"[DEBUG] Extracted all {listItems.Count} li elements");
                }
                Console.WriteLine("[DEBUG] ===== FINISHED EXTRACTING TEXT =====");
                
                // Decode HTML entities and clean up
                var plainText = System.Net.WebUtility.HtmlDecode(extractedText.Trim());
                
                // Remove reference markers like [1], [2], etc. and collapse whitespace
                plainText = System.Text.RegularExpressions.Regex.Replace(plainText, @"\[\d+\]", " ");
                plainText = System.Text.RegularExpressions.Regex.Replace(plainText, @"\s+", " ").Trim();
                
                Console.WriteLine($"[DEBUG] ===== FINAL COMBINED TEXT =====");
                Console.WriteLine($"[DEBUG] Total Length: {plainText.Length} characters");
                Console.WriteLine("[DEBUG] Complete Text:");
                Console.WriteLine(plainText);
                Console.WriteLine("[DEBUG] ===== END OF TEXT =====");

                var normalizedText = TextNormalizer.Normalize(plainText);
                Console.WriteLine($"[DEBUG API] ===== normalizedText ===== {normalizedText}");
                return normalizedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching section from MediaWiki API: {ex.Message}", ex);
            }
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
