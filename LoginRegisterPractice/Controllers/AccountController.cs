using LoginRegisterPractice.Models;
using LoginRegisterPractice.Utilities;
using LoginRegisterPractice.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LoginRegisterPractice.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser
            {
                Fullname = register.FullName,
                Email = register.Email,
                UserName = register.Username
            };
            if (user == null) return BadRequest();
            IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);
            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(VerifyEmail), "Account", new { userid = user.Id, token },
                                                                                Request.Scheme, Request.Host.ToString());
                Email.SendEmailAsync(_configuration.GetSection("MailSettings:Mail").Value, 
                                     _configuration.GetSection("MailSettings:Password").Value,
                                     user.Email, link, "ConfirmationLink");
                return View();
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }
    }
}
