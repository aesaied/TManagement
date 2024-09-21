using Microsoft.AspNetCore.Mvc;
using System.Data;
using TManagement.AppServices;
using TManagement.AppServices.Account;

namespace TManagement.Controllers
{
    public class UsersController(IAccountAppService accountAppService) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetJsonList([FromForm]DataTableFilterInput input)
        {

            var  column= Request.Form["order[0][column]"];
            var  name = Request.Form[$"columns[{column}][name]"];
            input.OrderByColumn = name;
            var  data = await accountAppService.GetPagedResult(input);

            return Json(data);
        }

        [HttpGet]

        public async Task<IActionResult> ChangeStatus(int id)
        {
            //  get User
            //  Get  Allowed Statuses

            var  user  = await accountAppService.GetById(id);

            if (user == null) {

                return BadRequest("Unable to find user!");
               /// return RedirectToAction(nameof(Index));
            
            }


            return View(user);

            


        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto input)
        {
            if (ModelState.IsValid) { 
            
                var  result =await accountAppService.ChangeStatus(input);

                if (result.Success)
                {

                    return Ok();
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                   
                }

              
            }
            return BadRequest(ModelState);

        }
    }
}
