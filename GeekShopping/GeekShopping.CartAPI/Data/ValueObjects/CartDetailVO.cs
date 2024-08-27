using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Data.ValueObjects
{
    public class CartDetailVO : BaseEntity
    {
        public long CartHeaderId { get; set; }

        public CartHeaderVO CartHeader { get; set; }

        public long ProductId { get; set; }

        public CartVO Product { get; set; }

        public int Count { get; set; }
    }
}
