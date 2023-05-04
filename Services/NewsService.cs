using ClimaCast.Data;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ClimaCast.Services
{
    public class NewsService
    {
        private readonly IConfiguration _config;

        public NewsService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<NewsData>> GetNewsDataAsync()
        {
            string apiKey = _config.GetValue<string>("NewsApiKey");
            string apiUrl = $"https://newsapi.org/v2/top-headlines?country=ph&apiKey={apiKey}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "localhost");

            var response = await client.GetAsync(apiUrl);

            var readAsStringAsync = response.Content.ReadAsStringAsync();
            Debug.WriteLine(readAsStringAsync.Result);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                // JObject.Parse() method is used to convert the JSON string into a JObject instance,
                // which allows us to easily extract data from the JSON object using the[] index operator.
                JObject data = JObject.Parse(content);

                List<NewsData> newsList = new List<NewsData>();

                // Access the array of articles from JSON response
                JArray articles = (JArray)data["articles"];

                foreach (JToken article in articles)
                {
                    Debug.WriteLine(article["source"]["name"].Value<string>());
                    NewsData newsData = new NewsData
                    {
                        Source = article["source"]["name"].Value<string>(),
                        Title = article["title"].Value<string>(),
                        Content = article["content"].Value<string>(),
                        Url = article["url"].Value<string>(),
                        ImageUrl = article["urlToImage"].Value<string>(),
                        // PublishedAt = DateTime.Parse(article["publishedAt"].Value<string>())
                    };
                    newsList.Add(newsData);
                }

                return newsList;
            }
            else
            {
                return null;
            }
        }
    }
}
