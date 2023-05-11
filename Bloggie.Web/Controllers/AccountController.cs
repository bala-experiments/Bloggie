using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            var identityuser = new IdentityUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

           var IdentityUserResult= await userManager.CreateAsync(identityuser,registerViewModel.Password);

            if (IdentityUserResult.Succeeded)
            {
               var RoleResult = await userManager.AddToRoleAsync(identityuser, "User");

                if (RoleResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel,string ReturnUrl)
        {
           var loginresult = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
           
            if (loginresult.Succeeded)
            {
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index","Home");
            }
            
            return View();
        }

        
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
