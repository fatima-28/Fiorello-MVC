using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ViewModels.Accounts;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager { get;  }
        public SignInManager<AppUser> _signInManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

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
            await _userManager.AddToRoleAsync(newUser, Roles.Role.User.ToString());
          await  _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
           await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInVM user)
        {
          AppUser userDb= await _userManager.FindByEmailAsync(user.email);
            if (userDb==null)
            {
                ModelState.AddModelError("","Your email or password is invalid");
                return View(user);
            }
            await _signInManager.SignInAsync(userDb, true);
            return RedirectToAction("Index", "Home");
        }
        //public async Task CreateRole()
        //{
        //    foreach (var item in Enum.GetValues(typeof(Roles.Role)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(item.ToString()))
        //        {
        //            IdentityRole role = new IdentityRole();
        //            role.Name = item.ToString();
        //            await _roleManager.CreateAsync(role);
        //        }
               
        //    }
           

        //}
    }
}
