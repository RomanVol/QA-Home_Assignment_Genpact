using Microsoft.Playwright;
using WikipediaPlaywrightTests.Utils;

namespace WikipediaPlaywrightTests.PageObjects
{
    public class WikipediaPlaywrightPage : BasePage
    {
        // Page-specific locators
        private const string H3DebuggingFeatures = "h3#Debugging_features";
        private const string DivMicrosoftDevTools = "div[aria-labelledby*='Microsoft&#95;development&#95;tools6288']";
        
        public WikipediaPlaywrightPage(IPage page) : base(page)
        {
        }
        
        /// <summary>
        /// Gets the text content of the Debugging features section via UI
        /// </summary>
        public async Task<string> GetDebuggingFeaturesTextWithUI()
        {
            try
            {
                // Wait for the page to load
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Step 1: Find the h3#Debugging_features element
                var h3Element = Page.Locator(H3DebuggingFeatures);
                await h3Element.WaitForAsync();
                
                Console.WriteLine($"[DEBUG] h3 element count: {await h3Element.CountAsync()}");
                Console.WriteLine($"[DEBUG] h3 element text: {await h3Element.TextContentAsync()}");
                
                // Step 2: Find the parent div of h3#Debugging_features
                var parentDiv = h3Element.Locator("xpath=ancestor::div[1]");
                
                Console.WriteLine($"[DEBUG] parentDiv count: {await parentDiv.CountAsync()}");
                try 
                {
                    var parentTag = await parentDiv.EvaluateAsync<string>("el => el.tagName");
                    Console.WriteLine($"[DEBUG] parentDiv tag: {parentTag}");
                    var parentClass = await parentDiv.GetAttributeAsync("class");
                    Console.WriteLine($"[DEBUG] parentDiv class: {parentClass}");
                    var parentHTML = await parentDiv.InnerHTMLAsync();
                    Console.WriteLine($"[DEBUG] parentDiv HTML (first 300 chars): {parentHTML.Substring(0, Math.Min(300, parentHTML.Length))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Error getting parentDiv info: {ex.Message}");
                }
                
                // Step 3: Find the p element under the parent div
                var pElement = parentDiv.Locator("xpath=following::p[1]");
                
                Console.WriteLine($"[DEBUG] pElement count: {await pElement.CountAsync()}");
                try
                {
                    var pTextDebug = await pElement.TextContentAsync();
                    Console.WriteLine($"[DEBUG] pElement text preview: {pTextDebug?.Substring(0, Math.Min(100, pTextDebug.Length))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Error getting pElement text: {ex.Message}");
                }
                
                // Step 4: Find the ul element that comes after the p element
                var ulElement = pElement.Locator("xpath=following::ul[1]");
                
                Console.WriteLine($"[DEBUG] ulElement count: {await ulElement.CountAsync()}");
                try
                {
                    var ulTextDebug = await ulElement.TextContentAsync();
                    Console.WriteLine($"[DEBUG] ulElement text preview: {ulTextDebug?.Substring(0, Math.Min(100, ulTextDebug.Length))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Error getting ulElement text: {ex.Message}");
                }
                
                // Extract text from p element
                var pText = await pElement.TextContentAsync() ?? "";
                Console.WriteLine($"[DEBUG UI] P element text: {pText.Substring(0, Math.Min(100, pText.Length))}...");
                
                // Extract all text from ul element (all li elements)
                var ulText = await ulElement.TextContentAsync() ?? "";
                Console.WriteLine($"[DEBUG UI] UL element text: {ulText.Substring(0, Math.Min(100, ulText.Length))}...");
                
                // Combine both texts
                var combinedText = (pText.Trim() + " " + ulText.Trim()).Trim();
                
                Console.WriteLine($"[DEBUG UI] ===== FINAL COMBINED TEXT =====");
                Console.WriteLine($"[DEBUG UI] Total Length: {combinedText.Length} characters");
                Console.WriteLine("[DEBUG UI] Complete Text:");
                Console.WriteLine(combinedText);
                Console.WriteLine("[DEBUG UI] ===== END OF TEXT =====");
                
                if (string.IsNullOrWhiteSpace(combinedText))
                {
                    throw new Exception("Section content is empty");
                }
                var normalizedText = TextNormalizer.Normalize(combinedText);
                Console.WriteLine($"[DEBUG UI] ===== normalizedText ===== {normalizedText}");
                return normalizedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to extract Debugging features section: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Gets technology names from Microsoft development tools section
        /// </summary>
        public async Task<List<(string Name, bool IsLink)>> GetMicrosoftDevToolsTechnologies()
        {
            try
            {
                // Wait for the page to load
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Find the div element with aria-labelledby containing "Microsoft_development_tools"
                var divLocator = Page.Locator(DivMicrosoftDevTools);
                
                await divLocator.WaitForAsync();
                
                Console.WriteLine($"[DEBUG] Found div with aria-labelledby: {await divLocator.CountAsync()}");
                
                // Find and click the button inside this div
                var buttonLocator = divLocator.Locator("button");
                var buttonCount = await buttonLocator.CountAsync();
                Console.WriteLine($"[DEBUG] Found {buttonCount} button(s) inside the div");
                
                if (buttonCount > 0)
                {
                    await buttonLocator.ClickAsync();
                    Console.WriteLine("[DEBUG] Clicked the button inside Microsoft_development_tools div");
                    
                    // Wait for content to load after clicking
                    await Page.WaitForTimeoutAsync(1000);
                }
                
                // Find all <a> elements inside the div
                var linkLocators = divLocator.Locator("a");
                var linkCount = await linkLocators.CountAsync();
                Console.WriteLine($"[DEBUG] Found {linkCount} <a> element(s) inside the div");
                
                var result = new List<(string Name, bool IsLink)>();
                
                // Check each <a> element for href attribute
                for (int i = 0; i < linkCount; i++)
                {
                    var link = linkLocators.Nth(i);
                    var text = await link.TextContentAsync();
                    var href = await link.GetAttributeAsync("href");
                    
                    var hasHref = !string.IsNullOrEmpty(href);
                    
                    Console.WriteLine($"[DEBUG] Link {i + 1}: Text='{text?.Trim()}', HasHref={hasHref}, Href='{href}'");
                    
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        result.Add((text.Trim(), hasHref));
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to extract Microsoft development tools: {ex.Message}", ex);
            }
        }
        

    }
}
