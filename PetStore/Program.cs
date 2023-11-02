
using PetStore;
using PetStore.Models;
using PetStore.Services;

//Welcome screen
ConsoleTablePrinter.PrintWelcomeScreen();
string BaseUrl = Console.ReadLine();
while (string.IsNullOrWhiteSpace(BaseUrl))
{
    Console.Write("Please enter base URL, it cannot be blank: ");
    BaseUrl = Console.ReadLine();
}


try
{
    var httpClient = new HttpClient
    {

        BaseAddress = new Uri(BaseUrl),
        Timeout = TimeSpan.FromSeconds(30)
    };

    IApiClient petStoreApiClient = new PetStoreApiClient(httpClient);
    var availablePets = await petStoreApiClient.GetPetsAsync("findByStatus?status=available");
    var sortedPets = availablePets
                           .OrderBy(pet => pet.Category?.Name)
                           .ThenByDescending(pet => pet.Name).ToList();


    ConsoleTablePrinter.WriteTableHeaders();

    const int pageSize = 10;
    bool displayAll = false;

    for (int i = 0; i < sortedPets.Count; i++)
    {
        if (i > 0 && i % pageSize == 0 && !displayAll)
        {
            Console.Write("Type 'all' to display all pets, or press any other key to continue: ");
            string input = Console.ReadLine();
            displayAll = input.Equals("all", StringComparison.OrdinalIgnoreCase);

            if (!displayAll)
            {
                Console.Clear();
                ConsoleTablePrinter.WriteTableHeaders();
            }
        }

        var pet = sortedPets[i];
        string tags = string.Join(", ", pet.Tags.Select(t => t.Name));
        Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-10}", pet.Category?.Name, pet.Name, pet.Status, tags );

        if (displayAll)
        {
            continue;
        }
    }

    Console.WriteLine("End of list. Press any key to exit.");
    Console.ReadKey();

}
catch (Exception e)
{
    Console.WriteLine($"An error occurred: {e.Message}");
}


