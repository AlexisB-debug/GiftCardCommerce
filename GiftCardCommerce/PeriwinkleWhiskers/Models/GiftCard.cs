namespace GiftCardCommerce.PeriwinkleWhiskers.Models;

public class GiftCard
{
    public int GiftCardId { get; set; }
    public required string GiftCardMerchant { get; set; }
    public required string GiftCardCode { get; set; }
    public decimal GiftCardBalance { get; set; }
    public bool GiftCardIsActive { get; set; }
    public DateTime GiftCardTimestamp { get; set; }
}