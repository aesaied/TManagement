using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TManagement.AppServices.Account;
using TManagement.Entities;
using TManagement.Services;

namespace TManagement.Controllers
{
    public class AccountController : Controller
    {

       
        public IActionResult Index()
        {
          
            return RedirectToActionPermanent(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto input,[FromServices] AppDbContext context )
        {

            if (ModelState.IsValid) 
            { 
                var  user = await context.Users.Include(s=>s.Groups).ThenInclude(g=>g.Group).FirstOrDefaultAsync(s=>s.Email == input.Email);

                if (user != null) {

                    PasswordHasher passwordHasher = new PasswordHasher();
                  bool result=  passwordHasher.VerifyPassword(input.Password, user.PasswordHash, user.PasswordSalt);

                    if (result)
                    {
                        if (user.CurrentStatus != UserStatus.Active) {

                            ModelState.AddModelError("", "User not active, contact system admin.");
                            return View(input);
                        }
                        else
                        {

                            var claims = new List<Claim>
                             {
                              new Claim(ClaimTypes.Name, user.Email!),
                              new Claim(ClaimTypes.Email, user.Email ?? "-"),
                            new Claim(ClaimTypes.GivenName,user.FullName??"-"),
                            };

                            foreach(var group  in user.Groups)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, group.Group.Name));
                            }

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var princibal = new ClaimsPrincipal(identity);



                            await this.HttpContext.SignInAsync(princibal,new AuthenticationProperties() {IsPersistent=input.RememberMe  });

                            return RedirectToAction("Index", "Home");

                        }
                    }
                    
                }


            
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(input);
        }
    }
}
