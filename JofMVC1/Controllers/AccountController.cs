using JofMVC1.Helpers;
using JofMVC1.Models;
using JofMVC1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JofMVC1.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<AppUser> _signInManager { get; }
        public UserManager<AppUser> _userManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }

        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
               Name= registerVM.Name,
               Surname=registerVM.Surname,
               Email=registerVM.Email,
               UserName=registerVM.Username
            };

            var result = await _userManager.CreateAsync(user,registerVM.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(" ", "Not Found");
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            if(user == null)
            {
                user=await _userManager.FindByNameAsync(login.UsernameOrEmail);
                if(user== null)
                {
                    ModelState.AddModelError(" ", "Not Found");
                }

            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(" ", "Not Found");
                return View();
            }

            if(result.IsLockedOut)
            {
                ModelState.AddModelError(" ", "Waiting...");
                return View();
            }

            await _signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach(UserRole item in Enum.GetValues(typeof(UserRole)))
            {
                if(await _roleManager.FindByNameAsync(item.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole() 
                    {
                        Name= item.ToString(),
                    });

                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
