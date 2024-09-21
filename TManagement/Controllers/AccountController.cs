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
    public class AccountController(ILoockupAppService _loockupsService, IAccountAppService accountAppService) : Controller
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

        [HttpPost]

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountAppService.Register(registerDto);

                if (result.Success)
                {
                    this.SetMessage("Your account is registered Successfully!!");
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {

                    ModelState.AddModelError("", error);
                }
            }

           await FillLoockups(registerDto.CountryId);
            return View(registerDto);

            

        }

        private async Task FillLoockups(Guid? countryId=null)
        {
            var  countries= await _loockupsService.GetLoockupList(LookupType.Country);
           
            var eduLevel = await _loockupsService.GetLoockupList(LookupType.EducationLevel);

            var  selCountry= new SelectList(countries, nameof(LoockupDto.Id), nameof(LoockupDto.Name));

            var  lst = selCountry.ToList();
            //lst.Insert(0, new SelectListItem("--select--", "-1",false));
            ViewBag.Country = lst;
            if (countryId.HasValue)
            {
                var cities = await _loockupsService.GetLoockupList(LookupType.City);
                ViewBag.Cities = new SelectList(cities.FindAll(s => s.FatherLookupId == countryId), nameof(LoockupDto.Id), nameof(LoockupDto.Name));
            }
            else
            {
                ViewBag.Cities = new List<SelectListItem>();
            }
            ViewBag.EducucationLevels = new SelectList(eduLevel, nameof(LoockupDto.Id), nameof(LoockupDto.Name));


        }


        public async Task<IActionResult> GetCities(Guid countryId)
        {
            var cities = await _loockupsService.GetLoockupList(LookupType.City);

            return Json( cities.Where(s=>s.FatherLookupId == countryId).ToList());

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));

        }


    }
}
