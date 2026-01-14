namespace SleepingBearCinema;

public class GiftCard
{
    public GiftCard()
    {
    }

    public int GiftCardID { get; set; }
    public string GiftCardMerchant { get; set; }
    public string GiftCardCode { get; set; }
    public decimal GiftCardBalance { get; set; }
    public bool GiftCardIsActive { get; set; }
    public DateTime GiftCardTimestamp { get; set; }
    // public DateTime GiftCardExpiration
    // {
    //     get { return GiftCardTimestamp.AddMonths(validityPeriodInMonths); }
    // }
}