using Microsoft.Extensions.Configuration;

namespace weatherNoAi;

class Program
{
    static async Task Main(string[] args)
    {
        DateTime now = DateTime.Now;
        
        Console.WriteLine($"Welcome to weatherNoai!{Environment.NewLine} {now}");
        string? cityInput;
        
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

        Console.WriteLine($"Selected city {cityInput}.");

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
            $"&q={Uri.EscapeDataString(cityInput!)}";
        
        
        using HttpClient client = new();
        
        using HttpResponseMessage response = await client.GetAsync(url);
        
        string responseBody = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(
            $"HTTP status: {(int)response.StatusCode} {response.StatusCode}");
        
        Console.WriteLine("API Response:");
        Console.WriteLine(responseBody);
        
    }
}
