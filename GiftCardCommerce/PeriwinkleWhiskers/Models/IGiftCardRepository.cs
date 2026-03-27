using GiftCardCommerce.PeriwinkleWhiskers.Models;

namespace GiftCardCommerce.PeriwinkleWhiskers.Models;

public interface IGiftCardRepository
{
    GiftCard? GetGiftCard(string giftCardCode);
    void SaveGiftCard(GiftCard giftCard);
}