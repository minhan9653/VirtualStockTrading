using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VirtualStockTrading.Model;

namespace VirtualStockTrading.Servies
{
    public class StockService
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "2BJB6JHALMQCE0YI."; //
        private const string BASE_URL = "https://www.alphavantage.co/query";

        public StockService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Stock> GetStocckAsync(string symbol)
        {
            string url = $"{BASE_URL}?function=GLOBAL_QUOTE&symbol={symbol}&apikey={API_KEY}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("API 요청 실패");

            string json = await response.Content.ReadAsStringAsync();
            var stockData = JsonSerializer.Deserialize<AlphaVantageResponse>(json);

            return new Stock
            {
                Symbol = stockData.GlobalQuote.Symbol,
                Name = symbol,  // Alpha Vantage API는 회사 이름을 제공하지 않음
                Price = stockData.GlobalQuote.Price,
                Change = stockData.GlobalQuote.Change
            };
        }


        public class AlphaVantageResponse
        {
            [JsonPropertyName("Global Quote")]
            public GlobalQuote GlobalQuote { get; set; }
        }

        public class GlobalQuote
        {
            [JsonPropertyName("01. symbol")]
            public string Symbol { get; set; }

            [JsonPropertyName("05. price")]
            public string PriceString { get; set; }

            [JsonIgnore]
            public double Price => double.TryParse(PriceString, out var result) ? result : 0;

            [JsonPropertyName("09. change")]
            public string ChangeString { get; set; }

            [JsonIgnore]
            public double Change => double.TryParse(ChangeString, out var result) ? result : 0;
        }



    }
}
