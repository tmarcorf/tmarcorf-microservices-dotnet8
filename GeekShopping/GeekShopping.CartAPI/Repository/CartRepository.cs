using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var header = await _context.CartHeaders.FirstOrDefaultAsync(
                c => c.UserId == userId);

            if (header != null)
            {
                header.CouponCode = couponCode;
                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(
                c => c.UserId == userId);

            if (cartHeader != null)
            {
                _context.CartDetails.RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));

                _context.CartHeaders.Remove(cartHeader);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            try
            {
                Cart cart = new Cart()
                {
                    CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(p => p.UserId == userId),
                };

                cart.CartDetails = _context.CartDetails
                    .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                    .Include(c => c.Product);

                return _mapper.Map<CartVO>(cart);
            }
            catch (AutoMapperMappingException ex)
            {
                Console.WriteLine("DEU RUIM ====> " + ex.Message);
                throw;
            }
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var header = await _context.CartHeaders.FirstOrDefaultAsync(
                c => c.UserId == userId);

            if (header != null)
            {
                header.CouponCode = "";
                _context.CartHeaders.Update(header);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetail);

                if (total == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cart)
        {
            var cartModel = _mapper.Map<Cart>(cart);

            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == cart.CartDetails.FirstOrDefault().ProductId);

            if (product == null)
            {
                _context.Products.Add(cartModel.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(
                c => c.UserId == cartModel.CartHeader.UserId);

            if (cartHeader == null)
            {
                _context.CartHeaders.Add(cartModel.CartHeader);
                await _context.SaveChangesAsync();
                
                cartModel.CartDetails.FirstOrDefault().CartHeaderId = cartModel.CartHeader.Id;
                cartModel.CartDetails.FirstOrDefault().Product = null;
                
                _context.CartDetails.Add(cartModel.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == cartModel.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    cartModel.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cartModel.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cartModel.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;

                    _context.CartDetails.Update(cartModel.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }


            return _mapper.Map<CartVO>(cartModel);
        }
    }
}
