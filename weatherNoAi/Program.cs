namespace weatherNoAi;

class Program
{
    static void Main(string[] args)
    {
        DateTime now = DateTime.Now;
        
        Console.WriteLine($"Welcome to weatherNoai!{Environment.NewLine} {now}");
        Console.Write("Enter your city: ");
        string? cityInput = Console.ReadLine();
    }
}
