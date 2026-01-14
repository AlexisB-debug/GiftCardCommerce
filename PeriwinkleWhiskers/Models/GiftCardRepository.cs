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

    public decimal SumConsignedGiftCards(string giftCardMerchant)
    {
        var sqlCreateTable = $@"CREATE TABLE periwinklewhiskers.{giftCardMerchant}ConsignedCards AS SELECT * FROM periwinklewhiskers.consign WHERE GiftCardMerchant = @giftCardMerchant";
        var parametersCreateTable = new { giftCardMerchantValue = giftCardMerchant };
        _connExecute(conn => conn.Execute(sqlCreateTable, parametersCreateTable));
        
        var SqlSumTable = $@"SELECT SUM(giftCardBalance) AS total_{giftCardMerchant}_balance FROM periwinklewhiskers.{giftCardMerchant}ConsignedCards";
        var parametersSumTable = new { giftCardMerchantValue = giftCardMerchant };
        var result = _connExecute(conn => conn.Execute(SqlSumTable, parametersSumTable));
        
        return result?.total_balance ?? 0m;
        
        // How to prevent the same cards from being recounted in the future?
        // After the balance is returned, the cards need to be deleted from the table periwinklewhiskers.consign to prevent a future recounted.
        var sqlDeleteJoinDrop = $@"DELETE periwinklewhiskers.consign FROM periwinkleWhiskers.consign AS A JOIN periwinklewhiskers.{giftCardMerchant}ConsignedCards " +
            "AS periwinklewhiskers.{giftCardMerchant}ConsignedCards ON perisinkleWhisker.consign.GiftCardCode = periwinklewhiskers.{giftCardMerchant}ConsignedCards";
        var parametersDeleteJoinDrop = new { giftCardMerchantValue = giftCardMerchant };
        _conExecute(conn => conn.Execute(sqlDeleteJoinDrop, parametersDeleteJoinDrop));
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