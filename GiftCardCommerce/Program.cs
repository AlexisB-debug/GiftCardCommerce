using GiftCardCommerce.PeriwinkleWhiskers.Models;
using GiftCardCommerce.PeriwinkleWhiskers.Services;
using Microsoft.Extensions.Configuration;

namespace GiftCardCommerce;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        string connectionString = configuration.GetConnectionString("PeriwinkleWhiskersConn")
            ?? throw new InvalidOperationException("No PeriwinkleWhiskers connection string found");
        DatabaseAccessFactory databaseAccessFactory = new DatabaseAccessFactory(connectionString);
        IGiftCardRepository repository = new GiftCardRepository(databaseAccessFactory);
        Console.WriteLine("=== Periwinkle Whiskers Gift Card Exchange ===");

        try
        {
            // get the merchant from the user
            Console.WriteLine("Which merchant is your gift card from?");
            Console.WriteLine("Options: Great Lakes Coffee, Petoskey Stone Petroleum, Robin Eggs Grocery, ");
            Console.WriteLine("Sleeping Bear Cinema, Sweet Crabapple Tavern, & White-Tailed Deer Department Store");
            Console.Write("\nPlease, type the merchant name: ");
            string? merchantName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(merchantName))
            {
                Console.WriteLine("Error: Merchant name is required.");
                return;
            }

            // get the gift card code
            Console.Write("Please type the gift card code: ");

            string? giftCardCode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(giftCardCode))
            {
                Console.WriteLine("Error: Gift card code is required.");
                return;
            }

            // validate the gift card (check if the gift card exists in the collaborator merchant's system)
            GiftCardCommerce.PeriwinkleWhiskers.Models.GiftCard? giftCard = repository.GetGiftCard(giftCardCode);

            if (giftCard == null)
            {
                Console.WriteLine($"Error: Gift card '{giftCardCode}' could not be found.");
                return;
            }

            if (!giftCard.GiftCardIsActive)
            {
                Console.WriteLine($"Error: Gift card '{giftCardCode}' is not active.");
                return;
            }

            // display gift card details and calculate payout (70% of balance)
            Console.WriteLine("\n--- Gift Card Details ---");
            Console.WriteLine($"Merchant: {giftCard.GiftCardMerchant}");
            Console.WriteLine($"Code: {giftCard.GiftCardCode}");
            Console.WriteLine($"Balance: {giftCard.GiftCardBalance:F2}");

            decimal payoutAmount = Math.Floor(0.70m * giftCard.GiftCardBalance * 100m) / 100m;
            Console.WriteLine($"Your payout (70%): {payoutAmount:F2}");

            // confirm the transaction
            Console.WriteLine("\nDo you want to sell this gift card? (yes/no): ");
            string? confirmation = Console.ReadLine();

            if (confirmation?.ToLower() != "yes")
            {
                Console.WriteLine("Transaction canceled");
                return;
            }

            // save the gift card to the "consign table"
            repository.SaveGiftCard(giftCard);
            Console.WriteLine($"\nSuccess! ${payoutAmount:F2} has been deposited into your account.");
            Console.WriteLine($"Gift card '{giftCard.GiftCardCode}' is now in our consignment inventory.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            Console.WriteLine($"\nStack Trace: {ex.StackTrace}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}