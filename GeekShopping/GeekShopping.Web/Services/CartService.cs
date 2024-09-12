using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;
using System.Reflection;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string BASE_PATH = "api/v1/cart";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BASE_PATH}/find-cart/{userId}");

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BASE_PATH}/add-cart", cart);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<CartViewModel> UpdateItemToCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson($"{BASE_PATH}/update-cart", cart);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartViewModel>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BASE_PATH}/remove-cart/{cartId}");

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<bool> ApplyCoupon(CouponViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BASE_PATH}/apply-coupon", model);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BASE_PATH}/remove-coupon/{userId}");

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<CartHeaderViewModel> Checkout(CartHeaderViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BASE_PATH}/checkout", model);

            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CartHeaderViewModel>();

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
