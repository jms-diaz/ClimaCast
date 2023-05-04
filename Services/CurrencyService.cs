using ClimaCast.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClimaCast.Services
{
    public class CurrencyService
    {
        private readonly IConfiguration _config;

        public CurrencyService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<CurrencyData>> GetCurrencyDataAsync()
        {
            string apiKey = _config.GetValue<string>("ExchangeRatesApiKey");
            string apiUrl = "https://api.apilayer.com/exchangerates_data/latest?&base=usd";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<CurrencyData> currencies = new List<CurrencyData>();
                JObject exchangeRates = JObject.Parse(content);

                foreach (JProperty currency in exchangeRates["rates"].Value<JObject>().Properties())
                {
                    string currencySymbol = currency.Name;
                    decimal rate = currency.Value.Value<decimal>();
                    CurrencyData currencyData = new CurrencyData
                    {
                        Currency = currencySymbol,
                        Rate = rate
                    };
                    currencies.Add(currencyData);
                };

                return currencies;
            } else
            {
                return null;
            }

        }
    }
}
