using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TManagement.AppServices.Account;
using TManagement.AppServices.Loockups;
using TManagement.Entities;
using TManagement.Services;

namespace TManagement.Controllers
{
    public class AccountController(ILoockupAppService _loockupsService) : Controller
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


        [HttpGet]
        public async Task<IActionResult> Register()
        {
           await FillLoockups();

            return View();
        }

        private async Task FillLoockups()
        {
            var  countries= await _loockupsService.GetLoockupList(LookupType.Country);
            var cities = await _loockupsService.GetLoockupList(LookupType.City);
            var eduLevel = await _loockupsService.GetLoockupList(LookupType.EducationLevel);

            var  selCountry= new SelectList(countries, nameof(LoockupDto.Id), nameof(LoockupDto.Name));

            var  lst = selCountry.ToList();
            lst.Insert(0, new SelectListItem("--select--", "-1",false));
            ViewBag.Country = lst;
            ViewBag.Cities = new SelectList(cities, nameof(LoockupDto.Id), nameof(LoockupDto.Name));
            ViewBag.EducucationLevels = new SelectList(eduLevel, nameof(LoockupDto.Id), nameof(LoockupDto.Name));


        }


        public async Task<IActionResult> GetCities(Guid countryId)
        {
            var cities = await _loockupsService.GetLoockupList(LookupType.City);

            return Json( cities.Where(s=>s.FatherLookupId == countryId).ToList());

        }


    }
}
