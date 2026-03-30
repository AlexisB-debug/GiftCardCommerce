using GiftCardCommerce.PeriwinkleWhiskers.Models;
using GiftCardCommerce.PeriwinkleWhiskers.Services;

namespace GiftCardCommerce.Tests;

public class GiftCardRepositoryTests
{
    private readonly IGiftCardRepository _giftCardRepository;

    public GiftCardRepositoryTests()
    {
        string connectionString = Environment.GetEnvironmentVariable("PeriwinkleWhiskersConn")
                                  ?? throw new InvalidOperationException(
                                      "PeriwinkleWhiskersConn environment variable not found");

        _giftCardRepository = new GiftCardRepository(new DatabaseAccessFactory(connectionString));
    }

    [Fact]
    public void GetGiftCard_WithValidCode_ReturnsGiftCard()
    {
        GiftCard? giftCard = _giftCardRepository.GetGiftCard("GLC-A1B2C3");

        Assert.NotNull(giftCard);
        Assert.Equal("GLC-A1B2C3", giftCard.GiftCardCode);
        Assert.Equal("Great Lakes Coffee", giftCard.GiftCardMerchant);
        Assert.True(giftCard.GiftCardIsActive);
    }

    [Fact]
    public void GetGiftCard_WithInvalidCode_ReturnsNull()
    {
        GiftCard? giftCard = _giftCardRepository.GetGiftCard("DoesNotExist");

        Assert.Null(giftCard);
    }

    [Fact]
    public void SaveGiftCard_ValidCard_ReturnsGiftCard_DoesNotThrow()
    {
        GiftCard card = new GiftCard
        {
            GiftCardMerchant = "Great Lakes Coffee",
            GiftCardCode = $"Test-{Guid.NewGuid().ToString()[..6].ToUpper()}",
            GiftCardBalance = 25.00m,
            GiftCardIsActive = true,
            GiftCardTimestamp = DateTime.Now,
        };

        _giftCardRepository.SaveGiftCard(card);
    }
}