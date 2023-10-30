using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository,UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var userViewModel = new UserViewModel();
            userViewModel.UsersList = new List<User>();

            if(users!= null)
            {
                foreach (var user in users)
                {
                    var useritem = new User { Id = Guid.Parse(user.Id), Username = user.UserName, EmailAddress = user.Email };
                    userViewModel.UsersList.Add(useritem);
                }
            }
            

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserViewModel request)
        {
            var identityuser = new IdentityUser { 
                UserName = request.UserName,
                Email = request.EmailAddress            
            };

            bool isadminchecked = request.IsAdminUser;

           var identityResult = await userManager.CreateAsync(identityuser,request.Password);

            if (identityResult.Succeeded && identityResult is not null)
            {
                var roles = new List<string> { "User" };
                if (request.IsAdminUser)
                {
                    roles.Add("Admin");
                }

                await userManager.AddToRolesAsync(identityuser, roles);

                //identityResult  = await userManager.AddToRolesAsync(identityuser, roles);

                //if (identityResult.Succeeded && identityResult is not null)
                //{
                //    return RedirectToAction("List");
                //}

            }

            return RedirectToAction("List");


        }
    }
}
