using ClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMart.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PetMart.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Client> _signInManager;
        private readonly UserManager<Client> _userManager;

        public AccountController(SignInManager<Client> signInManager, UserManager<Client> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", "Account");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username,
                model.Password, true, false);
                if (result.Succeeded)
                {
                    ViewData["LoginError"] = "False";
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewData["LoginError"] = "True";
            return View("Index");
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client user = new()
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return await Login(model);
                }
            }
            return View("SignUp", "Account");
        }
    }
}
