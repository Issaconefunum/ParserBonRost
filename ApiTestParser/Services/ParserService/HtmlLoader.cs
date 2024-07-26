using ApiTestParser.ConfigurationManager;
//using Elastic.CommonSchema;
using System.Net;

namespace ApiTestParser.Services.ParserService
{
    public static class HtmlLoader
    {
        private static SocketsHttpHandler socketHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) };
        private static HttpClient _httpClient = new HttpClient(socketHandler);
        public static async Task<string> GetSourceByPageId(string url, string urlProduct = null)
        {
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            if (urlProduct != null) url = "https://bonrost.ru/" + urlProduct;
            var response = await _httpClient.GetAsync(url);
            string source = string.Empty;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
