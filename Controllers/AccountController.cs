using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels.Accounts;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager { get;  }
        public SignInManager<AppUser> _signInManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountVm user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            AppUser newUser = new AppUser
            {
                Name = user.name,
                UserName=user.UserName,
                Email = user.email
            };
            var result = await _userManager.CreateAsync(newUser, user.password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(user);
            }
            return View();
          await  _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Home","Index");
        }
        public async Task<IActionResult> Logout()
        {
           await  _signInManager.SignOutAsync();
            return RedirectToAction("Home", "Index");
        }
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInVM user)
        {
          AppUser userDb= await _userManager.FindByIdAsync(user.email);
            if (userDb==null)
            {
                ModelState.AddModelError("","Uour email or password is invalid");
                return View(user);
            }
            return RedirectToAction("Home", "Index");
        }
    }
}
