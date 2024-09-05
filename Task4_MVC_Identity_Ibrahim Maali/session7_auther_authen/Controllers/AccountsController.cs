using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session7_auther_authen.Data;
using session7_auther_authen.Models.ViewModels;

namespace session7_auther_authen.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signManager;

        public AccountsController(ApplicationDbContext dbContext,UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager) {

            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signManager = signManager;
        }

        [HttpGet]
        public IActionResult Register( )
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                // Log the errors or show them on the view
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If we reach here, something went wrong
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
