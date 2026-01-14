using System.Text.Json;
using PeriwinkleWhiskers.Models;
using SleepingBearCinema;

namespace PeriwinkleWhiskers;

class Program
{
    static void Main(string[] args)
    {
        // This is a user selling his/her gift card(s) to Periwinkle Whiskers.
        Console.WriteLine(
            "Please, choose a database: Great Lakes Coffee, Petoskey Stone Petroleum, Robin Eggs Grocery," +
            "Sleeping Bear Cinema, Sweet Crabapple Tavern, White-Tailed Deer Department Store");
        string targetDatabase = DatabaseDropdownMenu();
        
        IDatabaseConnection databaseAccess = DatabaseAccessFactory.GetTargetDatabaseAccess(targetDatabase);
        
        string giftCardCode = Console.ReadLine();

        var GiftCard = GiftCardRepository.SaveGiftCard(giftCardCode);
        
        // flea market stand is the complement of shopping cart.
        List<GiftCard> fleaMarketStand =  new List<GiftCard>();
        fleaMarketStand.Add(GiftCardRepository.GetGiftCard(giftCardCode));
        
        // This method will save the gift card to the table PeriwinkleWhiskers.consign
        GiftCardRepository.SaveGiftCard(GiftCard);
        
        decimal check = 0.0m
        foreach (decimal balance in fleaMarketStand)
        {
            check = check + (Math.Floor(.70m * balance * 100m) / 100m);
        }
        
        Console.WriteLine($"Deposited {check} into account or mailed check of {check} through snail mail");
        
        // The method SumConsignedGiftCards will be called bi-weekly or each month.
        // This is separate from the above user transaction.
        // This transaction will convert the consigned gift cards & print new gift cards for sale on the self.
        decimal consignBalance = GiftCardRepository.SumConsignedGiftCards();
        // The variable consignBalance needs to be saved to a json flile & sent to the collaborator company
        // so that new gift cards can be created.
        string jsonString = JsonSerializer.Serialize(consignBalance, new JsonSerializerOptions await File.WriteAllTextAsync("consignBalance"));
        // The consignBalance was sent & a .json file with the new gift cards is received.
        // The next step is reading the .json file and inserting those giftcards into the periwinklewhiskers.giftcard
    }

    private static string DatabaseDropdownMenu()
    {
        // This is a temparay method so that I can test my program before I code a dropdown list.
        string targetDatabase = "SleepingBearDatabaseConn";
    }
}