namespace CouponsWeb.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public double DiscountAccount { get; set; }

        public int MinAccount { get; set; }
    }
}
