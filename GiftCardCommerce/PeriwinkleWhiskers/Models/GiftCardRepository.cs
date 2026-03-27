using GiftCardCommerce.PeriwinkleWhiskers.Models;
using GiftCardCommerce.PeriwinkleWhiskers.Services;
using MySqlConnector;
using System.Security.Cryptography;

namespace GiftCardCommerce.PeriwinkleWhiskers.Models;

public class GiftCardRepository : IGiftCardRepository
{
    private readonly DatabaseAccessFactory _databaseAccessFactory;
    public GiftCardRepository(DatabaseAccessFactory databaseAccessFactory)
    {
        _databaseAccessFactory = databaseAccessFactory;
    }

    public GiftCard? GetGiftCard(string giftCardCode)
    {
        using MySqlConnection conn = _databaseAccessFactory.CreateConnection();
        conn.Open();
        
        using MySqlCommand cmd = new("SELECT * FROM periwinklewhiskers.marketplace WHERE periwinklewhiskers.marketplace.GiftCardCode = @code AND periwinklewhiskers.marketplace.GiftCardIsActive = true", conn);
        cmd.Parameters.AddWithValue("@code", giftCardCode);
        
        using MySqlDataReader reader = cmd.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new GiftCard
        {
            GiftCardId = reader.GetInt32("GiftCardId"),
            GiftCardMerchant = reader.GetString("GiftCardMerchant"),
            GiftCardCode = reader.GetString("GiftCardCode"),
            GiftCardBalance = reader.GetDecimal("GiftCardBalance"),
            GiftCardIsActive = reader.GetBoolean("GiftCardIsActive"),
            GiftCardTimestamp = reader.GetDateTime("GiftCardTimestamp")
        };
    }

    public void SaveGiftCard(GiftCard giftCard)
    {
        using MySqlConnection conn = _databaseAccessFactory.CreateConnection();
        conn.Open();
        
        using MySqlCommand cmd = new("INSERT INTO periwinklewhiskers.consign (GiftCardId, GiftCardMerchant, GiftCardCode, GiftCardBalance, GiftCardIsActive, GiftCardTimestamp) VALUES (@id, @merchant, @code, @balance, @isActive, @timestamp)", conn);
        
        cmd.Parameters.AddWithValue("@id", giftCard.GiftCardId);
        cmd.Parameters.AddWithValue("@merchant", giftCard.GiftCardMerchant);
        cmd.Parameters.AddWithValue("@code", giftCard.GiftCardCode);
        cmd.Parameters.AddWithValue("@balance", giftCard.GiftCardBalance);
        cmd.Parameters.AddWithValue("@isActive", giftCard.GiftCardIsActive);
        cmd.Parameters.AddWithValue("@timestamp", giftCard.GiftCardTimestamp);
        
        cmd.ExecuteNonQuery();
    }
}