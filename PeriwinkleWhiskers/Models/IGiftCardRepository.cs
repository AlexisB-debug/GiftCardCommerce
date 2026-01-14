using SleepingBearCinema;

namespace PeriwinkleWhiskers.Models;

public interface IGiftCardRepository
{
    public GiftCard GetGiftCard(string giftCardCode);
    public void SaveGiftCard(GiftCard giftCard);
    public void DeleteGiftCard(string giftCardCode);

    public void SumConsignedGiftCards();
}