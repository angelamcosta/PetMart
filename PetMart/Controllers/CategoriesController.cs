using ClassLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLibrary;
using System.Collections.Generic;

namespace PetMart.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ItemsRepo _itemsRepo;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Client> _userManager;

        public CategoriesController(ItemsRepo itemsRepo, IConfiguration configuration, UserManager<Client> userManager)
        {
            _itemsRepo = itemsRepo;
            _configuration = configuration;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string getProducts = _configuration.GetValue<string>("UrlStrings:GetProducts");
            IEnumerable<Product> products = _itemsRepo.ReturnList<Product>(_itemsRepo.Get(getProducts));
            ViewBag.Category = "None";
            return View(products);
        }

        public IActionResult GetCategory(string id)
        {
            string getCategories = _configuration.GetValue<string>("UrlStrings:GetCategories") + id;
            Category category = _itemsRepo.ReturnObject<Category>(_itemsRepo.Get(getCategories));
            ViewBag.Category = category.Name;
            return View("Index", category.Products);
        }

        public IActionResult Search(string searchString)
        {
            string getProducts = _configuration.GetValue<string>("UrlStrings:GetProducts") + "Search/" + searchString;
            IEnumerable<Product> products = _itemsRepo.ReturnList<Product>(_itemsRepo.Get(getProducts));
            return View("Index", products);
        }

        public IActionResult Details(string id)
        {
            string getProducts = _configuration.GetValue<string>("UrlStrings:GetProducts") + id;
            Product product = _itemsRepo.ReturnObject<Product>(_itemsRepo.Get(getProducts));
            return View(product);
        }
    }
}
