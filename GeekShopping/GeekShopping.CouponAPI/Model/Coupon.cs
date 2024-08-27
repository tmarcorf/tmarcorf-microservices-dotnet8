using GeekShopping.CouponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Model
{
    [Table("coupon")]
    public class Coupon : BaseEntity
    {
        [Required]
        [Column("coupon_code")]
        [StringLength(30)]
        public string? CouponCode { get; set; }

        [Required]
        [Column("discount_amount")]
        [Range(1, 10000)]
        public decimal DiscountAmount { get; set; }
    }
}
