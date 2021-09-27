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
    public class OrdersController : Controller
    {
        private readonly ItemsRepo _itemsRepo;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Client> _userManager;

        public OrdersController(ItemsRepo itemsRepo, IConfiguration configuration, UserManager<Client> userManager)
        {
            _itemsRepo = itemsRepo;
            _configuration = configuration;
            _userManager = userManager;
        }

        public IActionResult Confirmation(int id)
        {
            string getOrders = _configuration.GetValue<string>("UrlStrings:GetOrders") + id;
            Order order = _itemsRepo.ReturnObject<Order>(_itemsRepo.Get(getOrders));
            return View(order);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            string getOrders = _configuration.GetValue<string>("UrlStrings:GetOrders") + "List/" + user.Id;
            IEnumerable<Order> orders = _itemsRepo.ReturnList<Order>(_itemsRepo.Get(getOrders));
            return View(orders);
        }

        public async Task<IActionResult> PlaceOrder(OrderViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            Order order = new()
            {
                Address = new Address { AddressLine1 = model.AddressLine1, AddressLine2 = model.AddressLine2, Country = model.Country, State = model.State, ZipCode = model.ZipCode, UserID = user.Id },
                FirstName = model.FirstName,
                LastName = model.LastName,
                Total = _itemsRepo.ReturnObject<decimal>(_itemsRepo.Get(_configuration.GetValue<string>("UrlStrings:GetShoppingItems") + "GetTotalCartSum/" + user.Id)),
                Items = new List<OrderItem>(),
                UserID = user.Id
            };
            string getShoppingItems = _configuration.GetValue<string>("UrlStrings:GetShoppingItems");
            string getList = getShoppingItems + user.Id;
            IEnumerable<ShoppingItem> shoppingItems = _itemsRepo.ReturnList<ShoppingItem>(_itemsRepo.Get(getList));
            foreach (var item in shoppingItems)
            {
                order.Items.Add(new OrderItem { ProductId = item.ProductId, Quantity = item.Quantity });
            }
            string getOrders = _configuration.GetValue<string>("UrlStrings:GetOrders");
            Order confirmedOrder = _itemsRepo.ReturnObject<Order> (_itemsRepo.Post(getOrders, order));
            string deleteCart = getShoppingItems + "ClearShoppingCart/" + user.Id;
            _itemsRepo.Delete(deleteCart);
            return RedirectToAction("Confirmation", new { id = confirmedOrder.Id });
        }
    }
}
