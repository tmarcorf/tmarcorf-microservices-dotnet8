using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface ICartService
    {
        Task<CartViewModel> FindCartByUserId(string userId, string token);
        Task<CartViewModel> AddItemToCart(CartViewModel cart, string token);
        Task<CartViewModel> UpdateItemToCart(CartViewModel cart, string token);
        Task<bool> RemoveFromCart(long cartId, string token);

        Task<CartViewModel> ApplyCoupon(CartViewModel cart, string couponCode, string token);
        Task<CartViewModel> RemoveCoupon(string userId, string token);
        Task<CartViewModel> ClearCart(string userId, string token);
        Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token);
    }
}
