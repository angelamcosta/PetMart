using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly PetMartContext _context;

        public ShoppingItemsController(PetMartContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingItems
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems(string id)
        {
            return await _context.ShoppingItems.Where(s => s.UserID.Equals(id)).ToListAsync();
        }

        // GET: api/ShoppingItems/5
        [HttpGet("GetItem/{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return shoppingItem;
        }

        // GET: api/ShoppingItems/5
        [HttpGet("GetTotalCartSum/{id}")]
        public decimal GetTotalCartSum(string id)
        {
            var shoppingItem = _context.ShoppingItems.Where(s => s.UserID.Equals(id)).Select(s => s.Product.Price * s.Quantity).Sum();

            return shoppingItem;
        }

        // PUT: api/ShoppingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShoppingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
        {
            _context.ShoppingItems.Add(shoppingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);
        }

        // DELETE: api/ShoppingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("ClearShoppingCart/{id}")]
        public async Task<IActionResult> ClearShoppingCart(string id)
        {
            _context.ShoppingItems.RemoveRange(_context.ShoppingItems.Where(x => x.UserID.Equals(id)));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ItemExists/{userId}/{itemId}")]
        public async Task<ActionResult<ShoppingItem>> ItemExists(string userId, int itemId)
        {
            var shoppingItem = await _context.ShoppingItems.Where(s => s.ProductId.Equals(itemId) && s.UserID.Equals(userId)).FirstOrDefaultAsync();

            return shoppingItem;
        }

        private bool ShoppingItemExists(int id)
        {
            return _context.ShoppingItems.Any(e => e.Id == id);
        }
    }
}
