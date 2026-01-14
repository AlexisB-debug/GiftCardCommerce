namespace SleepingBearCinema;

public class GiftCardRepository : IGiftCardRepository
{
    public void CreateGiftCard(decimal load)
    {
        _conn.Execute(
            "INSERT INTO SleepingBear.GiftCard (GiftCardID, GiftCardMerchant, GiftCardCode, GiftCardBalance, GiftCardIsActive, GiftCardTimestamp) VALUES (@giftCardID, @giftCardMerchant, @giftCardCode, @giftCardBalance, @giftCardIsActive, @giftCardTimestamp);",
            new
            {
                GiftCardMerchant = SleepingBearCinema, GiftCardCode = Guid.NewGuid().ToString(), GiftCardBalance = load,
                GiftCardIsActive = false, GiftCardTimestamp = DateTime.Now
            });
        // Insert into a .json file the same SQL that is above. The .json file will be sent to Periwinkle Whiskers.
        // Periwinkle Whiskers will insert the gift cards in that .json file into it's table periwinklewhiskers.giftcard.
        // I need to code a json class and an IjsonRepository interface.
    }
}