using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http;

namespace weatherNoAi;

class Program
{
    static async Task Main(string[] args)
    {
        DateTime now = DateTime.Now;
        
        Console.WriteLine($"Welcome to weatherNoai!{Environment.NewLine} {now}");
        string cityInput;
        
        do
        {
            Console.Write("Enter your city: ");
            cityInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cityInput))
            {
                Console.WriteLine("Empty value, please enter again.");
            }
        } 
        while (string.IsNullOrWhiteSpace(cityInput));
        
        string cityInputClean = cityInput.ToLower().Trim();
        
        Console.WriteLine($"Selected city {cityInputClean}.");
        Console.WriteLine("=== API STATUS ===");

        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
        
        string? apiKey = config["WeatherApi:Key"];
        
            
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("API Key is missing.");
            return;
        }
        
        string url =
             "https://api.weatherapi.com/v1/current.json" + 
            $"?key={Uri.EscapeDataString(apiKey)}" +
            $"&q={Uri.EscapeDataString(cityInputClean)}";
        
        
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Weather API returned error: " + response.StatusCode);
            Console.WriteLine(responseBody);
            return;
        }
        
        Console.WriteLine(
            $"HTTP status: {(int)response.StatusCode} {response.StatusCode}");
        
       
       using JsonDocument document = JsonDocument.Parse(responseBody);
       JsonElement root = document.RootElement;
       
       JsonElement location = root.GetProperty("location");
       string city = location.GetProperty("name").GetString()!;
       
       string country = location.GetProperty("country").GetString()!;
       
       JsonElement current = root.GetProperty("current");
       double temperatureC = current.GetProperty("temp_c").GetDouble();
       
       JsonElement condition = current.GetProperty("condition");
       string describeCondition = condition.GetProperty("text").GetString()!;

       Console.WriteLine("=== WEATHER FORECAST ===");
       
       Console.WriteLine($"Country: {country} {Environment.NewLine}City: {city}");
       Console.WriteLine($"Temperature: {temperatureC}°C");
       Console.WriteLine($"Condition: {describeCondition}");


    }
}
