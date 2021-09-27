using ClassLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetMart.Models;
using RepoLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetMart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ItemsRepo _itemsRepo;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Client> _userManager;

        public ShoppingCartController(ItemsRepo itemsRepo, IConfiguration configuration, UserManager<Client> userManager)
        {
            _itemsRepo = itemsRepo;
            _configuration = configuration;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult RemoveFromCart(int id)
        {
            string getShoppingItems = _configuration.GetValue<string>("UrlStrings:GetShoppingItems") + id;
            _itemsRepo.Delete(getShoppingItems);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateItem(int id, bool add = true)
        {
            string itemUrl = _configuration.GetValue<string>("UrlStrings:GetShoppingItems") + id;
            string getItemUrl = _configuration.GetValue<string>("UrlStrings:GetShoppingItems") + "GetItem/" + id;
            ShoppingItem shoppingItem = _itemsRepo.ReturnObject<ShoppingItem>(_itemsRepo.Get(getItemUrl));
            if (add)
            {
                shoppingItem.Quantity++;
            }
            else
            {
                if (shoppingItem.Quantity - 1 == 0)
                {
                    _itemsRepo.Delete(itemUrl);
                    return RedirectToAction("ShoppingCart");
                }
                else
                {
                    shoppingItem.Quantity--;
                }
            }
            _itemsRepo.Put(itemUrl, shoppingItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            string getShoppingItems = _configuration.GetValue<string>("UrlStrings:GetShoppingItems") + user.Id;
            IEnumerable<ShoppingItem> shoppingItems = _itemsRepo.ReturnList<ShoppingItem>(_itemsRepo.Get(getShoppingItems));
            CheckoutViewModel model = new() { Items = shoppingItems, CartTotal = _itemsRepo.ReturnObject<decimal>(_itemsRepo.Get(_configuration.GetValue<string>("UrlStrings:GetShoppingItems") + "GetTotalCartSum/" + user.Id)) };
            return View(model);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            string getItemUrl = _configuration.GetValue<string>("UrlStrings:GetShoppingItems") + "ItemExists/" + user.Id + "/" + id;
            ShoppingItem item = _itemsRepo.ReturnObject<ShoppingItem>(_itemsRepo.Get(getItemUrl));
            if (item == null)
            {
                ShoppingItem shoppingItem = new() { ProductId = id, UserID = user.Id, Quantity = 1 };
                string getShoppingItems = _configuration.GetValue<string>("UrlStrings:GetShoppingItems");
                _itemsRepo.Post(getShoppingItems, shoppingItem);
            }
            else
            {
                return RedirectToAction("UpdateItem", new { id = item.Id });
            }

            return RedirectToAction("Index");
        }
    }
}
