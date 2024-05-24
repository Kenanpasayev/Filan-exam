using FiNAL_imtahan.Models;
using FiNAL_imtahan.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FiNAL_imtahan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid) return View();
            User user = new User();
            {
                user.Email = registerVM.Email;
                user.Name = registerVM.Name;
                user.Surname = registerVM.Surname;
                user.UserName = registerVM.Username;
            };
            var resault = await _userManager.CreateAsync(user, registerVM.Password);
            if (!resault.Succeeded)
            {
                foreach (var item in resault.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginVM.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "username ve password yalnuisdir");
                return View();
            }
            var resault = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true);
            if (!resault.Succeeded)
            {

                ModelState.AddModelError("", "username ve password yalnuisdir");
                return View();
            }

            await _signInManager.SignInAsync(user, loginVM.RememberMe);
             return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
