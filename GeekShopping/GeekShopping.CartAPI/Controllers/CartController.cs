using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<ProductVO>> FindById(string userId)
        {
            var cart = await _repository.FindCartByUserId(userId);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        
        [HttpPost("add-cart")]
        public async Task<ActionResult<ProductVO>> AddCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        
        [HttpPut("update-cart")]
        public async Task<ActionResult<ProductVO>> UpdateCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        
        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<ProductVO>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);

            if (!status) return NotFound();

            return Ok(status);
        }
    }
}
