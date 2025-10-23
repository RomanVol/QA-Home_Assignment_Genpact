using System.Text.RegularExpressions;

namespace WikipediaPlaywrightTests.Utils
{
    public static class TextNormalizer
    {
        /// <summary>
        /// Normalizes text by removing special characters, extra whitespace, and converting to lowercase
        /// </summary>
        public static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            
            // Remove references like [1], [2], etc.
            text = Regex.Replace(text, @"\[\d+\]", string.Empty);
            
            // Remove special characters except spaces and alphanumeric
            text = Regex.Replace(text, @"[^\w\s]", " ");
            
            // Replace multiple spaces with single space
            text = Regex.Replace(text, @"\s+", " ");
            
            // Convert to lowercase and trim
            return text.ToLower().Trim();
        }
        
        /// <summary>
        /// Counts unique words in normalized text
        /// </summary>
        public static int CountUniqueWords(string normalizedText)
        {
            if (string.IsNullOrWhiteSpace(normalizedText))
                return 0;
            
            var words = normalizedText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var uniqueWords = new HashSet<string>(words);
            
            return uniqueWords.Count;
        }
        
        /// <summary>
        /// Gets unique words from normalized text
        /// </summary>
        public static HashSet<string> GetUniqueWords(string normalizedText)
        {
            if (string.IsNullOrWhiteSpace(normalizedText))
                return new HashSet<string>();
            
            var words = normalizedText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(words);
        }
    }
}
