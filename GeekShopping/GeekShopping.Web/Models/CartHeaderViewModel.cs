namespace GeekShopping.Web.Models
{
    public class CartHeaderViewModel : BaseEntity
    {
        public string UserId { get; set; }

        public string CouponCode { get; set; }

        public double PurchaseAmount { get; set; }
    }
}
