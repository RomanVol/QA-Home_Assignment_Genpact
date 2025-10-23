using Microsoft.Playwright;
using WikipediaPlaywrightTests.Utils;

namespace WikipediaPlaywrightTests.PageObjects
{
    /// <summary>
    /// Page Object for Wikipedia Playwright article
    /// Follows SOLID principles with focused, single-responsibility methods
    /// </summary>
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
        /// Orchestrates the extraction process following Single Responsibility Principle
        /// </summary>
        public async Task<string> GetDebuggingFeaturesTextWithUI()
        {
            try
            {
                await WaitForPageLoad();
                
                var h3Element = await FindDebuggingFeaturesHeading();
                var parentDiv = await FindParentDivOfHeading(h3Element);
                var pElement = await FindFollowingParagraph(parentDiv);
                var ulElement = await FindFollowingList(pElement);
                
                var combinedText = await ExtractCombinedText(pElement, ulElement);
                await LogExtractedText(combinedText);
                
                ValidateTextNotEmpty(combinedText);
                
                var normalizedText = TextNormalizer.Normalize(combinedText);
                
                return normalizedText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to extract Debugging features section: {ex.Message}", ex);
            }
        }
        
        #region Private Helper Methods - Debugging Features Extraction
        
        /// <summary>
        /// Waits for page to fully load (SRP: Page loading responsibility)
        /// </summary>
        private async Task WaitForPageLoad()
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        
        /// <summary>
        /// Finds the h3#Debugging_features heading element (SRP: Heading location)
        /// </summary>
        private async Task<ILocator> FindDebuggingFeaturesHeading()
        {
            var h3Element = Page.Locator(H3DebuggingFeatures);
            await h3Element.WaitForAsync();
            
            return h3Element;
        }
        
        /// <summary>
        /// Finds the parent div of the heading element (SRP: Parent element location)
        /// </summary>
        private async Task<ILocator> FindParentDivOfHeading(ILocator headingElement)
        {
            var parentDiv = headingElement.Locator("xpath=ancestor::div[1]");
            
            await LogParentDivDetails(parentDiv);
            
            return parentDiv;
        }
        
        /// <summary>
        /// Finds the paragraph element following the parent div (SRP: Paragraph location)
        /// </summary>
        private async Task<ILocator> FindFollowingParagraph(ILocator parentDiv)
        {
            var pElement = parentDiv.Locator("xpath=following::p[1]");
            
            await LogElementTextPreview(pElement, "pElement");
            
            return pElement;
        }
        
        /// <summary>
        /// Finds the list element following the paragraph (SRP: List location)
        /// </summary>
        private async Task<ILocator> FindFollowingList(ILocator paragraphElement)
        {
            var ulElement = paragraphElement.Locator("xpath=following::ul[1]");
            
            await LogElementTextPreview(ulElement, "ulElement");
            
            return ulElement;
        }
        
        /// <summary>
        /// Extracts and combines text from paragraph and list elements (SRP: Text extraction)
        /// </summary>
        private async Task<string> ExtractCombinedText(ILocator paragraphElement, ILocator listElement)
        {
            var pText = await paragraphElement.TextContentAsync() ?? "";
            var ulText = await listElement.TextContentAsync() ?? "";
            
            return (pText.Trim() + " " + ulText.Trim()).Trim();
        }
        
        /// <summary>
        /// Validates that extracted text is not empty (SRP: Validation)
        /// </summary>
        private void ValidateTextNotEmpty(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new Exception("Section content is empty");
            }
        }
        
        /// <summary>
        /// Logs details about parent div element (SRP: Diagnostic logging)
        /// </summary>
        private async Task LogParentDivDetails(ILocator parentDiv)
        {
            try 
            {
                var parentTag = await parentDiv.EvaluateAsync<string>("el => el.tagName");
                var parentClass = await parentDiv.GetAttributeAsync("class");
                var parentHTML = await parentDiv.InnerHTMLAsync();
            }
            catch
            {
                // Silently handle errors in logging
            }
        }
        
        /// <summary>
        /// Logs text preview of an element (SRP: Diagnostic logging)
        /// </summary>
        private async Task LogElementTextPreview(ILocator element, string elementName)
        {
            try
            {
                var text = await element.TextContentAsync();
            }
            catch
            {
                // Silently handle errors in logging
            }
        }
        
        /// <summary>
        /// Logs the final extracted text (SRP: Diagnostic logging)
        /// </summary>
        private async Task LogExtractedText(string text)
        {
            await Task.CompletedTask;
        }
        
        #endregion
        
        /// <summary>
        /// Gets technology names from Microsoft development tools section
        /// Orchestrates the extraction process following Single Responsibility Principle
        /// </summary>
        public async Task<List<(string Name, bool IsLink)>> GetMicrosoftDevToolsTechnologies()
        {
            try
            {
                await WaitForPageLoad();
                
                var divLocator = await FindMicrosoftDevToolsDiv();
                await ExpandCollapsibleSection(divLocator);
                var technologies = await ExtractTechnologiesFromDiv(divLocator);
                
                return technologies;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to extract Microsoft development tools: {ex.Message}", ex);
            }
        }
        
        #region Private Helper Methods - Microsoft Dev Tools Extraction
        
        /// <summary>
        /// Finds the Microsoft development tools div element (SRP: Element location)
        /// </summary>
        private async Task<ILocator> FindMicrosoftDevToolsDiv()
        {
            var divLocator = Page.Locator(DivMicrosoftDevTools);
            await divLocator.WaitForAsync();
            
            return divLocator;
        }
        
        /// <summary>
        /// Expands collapsible section by clicking button if present (SRP: UI interaction)
        /// </summary>
        private async Task ExpandCollapsibleSection(ILocator containerDiv)
        {
            var buttonLocator = containerDiv.Locator("button");
            var buttonCount = await buttonLocator.CountAsync();
            
            if (buttonCount > 0)
            {
                await buttonLocator.ClickAsync();
                
                // Wait for content to load after clicking
                await Page.WaitForTimeoutAsync(1000);
            }
        }
        
        /// <summary>
        /// Extracts all technology links from the container div (SRP: Data extraction)
        /// </summary>
        private async Task<List<(string Name, bool IsLink)>> ExtractTechnologiesFromDiv(ILocator containerDiv)
        {
            var linkLocators = containerDiv.Locator("a");
            var linkCount = await linkLocators.CountAsync();
            
            var technologies = new List<(string Name, bool IsLink)>();
            
            for (int i = 0; i < linkCount; i++)
            {
                var technology = await ExtractSingleTechnology(linkLocators.Nth(i), i);
                
                if (technology.HasValue)
                {
                    technologies.Add(technology.Value);
                }
            }
            
            return technologies;
        }
        
        /// <summary>
        /// Extracts technology name and link status from a single anchor element (SRP: Single item extraction)
        /// </summary>
        private async Task<(string Name, bool IsLink)?> ExtractSingleTechnology(ILocator linkElement, int index)
        {
            var text = await linkElement.TextContentAsync();
            var href = await linkElement.GetAttributeAsync("href");
            var hasHref = !string.IsNullOrEmpty(href);
            
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }
            
            return (text.Trim(), hasHref);
        }
        
        #endregion
    }
}
