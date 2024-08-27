using AutoMapper;
using GeekShopping.CouponAPI.Data.ValueObjetcs;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponVO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
