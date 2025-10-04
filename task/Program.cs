using System;
using System.Threading.Tasks;
using TaskAPI;

public class Program
{
    public static async Task Main()
    {
        var manager = DogCeo.Instance;

        manager.AddObsrever(url => Console.WriteLine(" Dog image Url 1: " + url));
        manager.AddObsrever(url => Console.WriteLine(" Dog image Url 2: " + url));

        Console.WriteLine(" Fetching dog image.");
        await manager.FetchDogImageAsync();
    }
}