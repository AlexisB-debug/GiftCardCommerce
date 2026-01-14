using System.Linq.Expressions;
using SleepingBearCinema;

namespace PeriwinkleWhiskers.Models;

public class GiftCardRepository : IGiftCardRepository
{
    string targetDatabase = DatabaseDropdownMenu();

    private readonly IDatabaseConnection _conn = DatabaseAccessFactory.GetTargetDatabaseAccess(targetDatabase);

    public GiftCardRepository()
    {
        _conn = _conn;
    }

    public GiftCard GetGiftCard(string giftCardCode)
    {
        try
        {
            GiftCard giftCard = _conn.QuerySingle<GiftCard>(
                "SELECT * FROM GiftCard WHERE GiftCardCode = @giftCardCode AND IsActive = true", new { giftCardCode });
            return giftCard;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    // flea market stand is the complement of shopping cart.
    // gift cards in the consign table: balance will be sumed and they will be exchanged for new gift cards to be placed on the shelf.

    public void SaveGiftCard(GiftCard giftCard)
    {
        _conn.Execute(
            "INSERT INTO periwinklewhiskers.consign (GiftCardMerchant, GiftCardCode, GiftCardBalance, GiftCardIsActive, GiftCardTimestamp) " +
            "VALUES (@giftCardMerchant, @giftCardCode, @giftCardBalance, @giftCardIsActive, @giftCardTimestamp);",
            new
            {
                giftCardMerchant = giftCard.GiftCardMerchant, giftCardCode = giftCard.GiftCardCode,
                giftCardBalance = giftCard.GiftCardBalance, giftCardIsActive = giftCard.GiftCardIsActive,
                giftCardTimestamp = giftCard.GiftCardTimestamp
            });
    }
    
    public void SumConsignedGiftCards()
    {
        throw new NotImplementedException();
    }

    // The user can delete the gift card from his/her flea market stand before cashing in.
    // Cash in is the complement of checkout.
    public void DeleteGiftCard(string giftCardCode)
    {
        _connExecute("DELETE FROM periwinklewhiskers.fleamarketstand WHERE GiftCardCode = @giftCardCode",
            new { giftCardCode });
    }

    private static string DatabaseDropdownMenu()
    {
        throw new NotImplementedException();
    }
}