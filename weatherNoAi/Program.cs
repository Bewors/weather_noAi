namespace weatherNoAi;

class Program
{
    static void Main(string[] args)
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
    }
}
