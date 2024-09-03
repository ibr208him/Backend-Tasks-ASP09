using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            //  var checkUser = dbContext.Users.FirstOrDefault(usr =>
            //                         usr.UserName == user.UserName && usr.Password == user.Password);
            var checkUser = dbContext.Users.Where(usr => usr.UserName == user.UserName && usr.Password == user.Password);

            if (checkUser.Any())
            {
                return RedirectToAction("Index", "Employees"); // redirect to action in another controller
            }
            ViewBag.Error = "Error in user name or password";
            return View(user); // return to same action with user data to e used to fikk the input fields
        }

        [HttpGet]
        public IActionResult GetInActiveUsers()
        {
            var inActiveUsers=dbContext.Users.Where(usr=>usr.IsActive==false || usr.IsActive == null).ToList();

            return View(inActiveUsers);
        }

      
        public IActionResult makeActive(Guid id)
        {
            var inactiveuser = dbContext.Users.FirstOrDefault(usr => usr.UserId == id);
            if (inactiveuser != null && (inactiveuser.IsActive == false || inactiveuser.IsActive == null))
            {
                inactiveuser.IsActive = true;
                dbContext.SaveChanges();

            }
            return RedirectToAction(nameof(GetInActiveUsers));

        }
    }
}
