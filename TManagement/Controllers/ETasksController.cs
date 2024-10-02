using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TManagement.AppServices.Account;
using TManagement.AppServices;
using TManagement.AppServices.ETasks;

namespace TManagement.Controllers
{
    public class ETasksController(IETasksAppService eTasksAppService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            await FillLookups();
            return View();
        }

        private async Task FillLookups()
        {

            var lst =await eTasksAppService.GetAllAllowedUsersToAssign();

            var userList = new SelectList(lst, nameof(TaskUserDto.Id), nameof(TaskUserDto.Name));

            ViewBag.UserList = userList;    
        }



        [HttpPost]

        public async Task<IActionResult> Create(CreateOrEditETaskDto input)
        {
            if (ModelState.IsValid)
            {
                var result =await eTasksAppService.CreateOrEdit(input);

                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);

                    }

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            await FillLookups();

            return View(input);
        }


        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetJsonList([FromForm] DataTableFilterInput input)
        {

            var column = Request.Form["order[0][column]"];
            var name = Request.Form[$"columns[{column}][name]"];
            input.OrderByColumn = name;
            var data = await eTasksAppService.GetPagedResult(input);

            return Json(data);
        }



        public async Task<IActionResult> Edit(int id)
        {

            var taskToEdit =await eTasksAppService.GetTaskToEdit(id);

            //  fill  users
            await FillLookups();

            return View(taskToEdit);

        }

        [HttpPost]

        public async Task<IActionResult> Edit(CreateOrEditETaskDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await eTasksAppService.CreateOrEdit(input);

                if (!result.Success)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);

                    }

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            await FillLookups();

            return View(input);
        }


        [HttpGet]

        public async Task<IActionResult> View(int id)
        {

            var  task =await  eTasksAppService.GetTaskToView(id);

            if (task == null)
            {
                return NotFound();
            }


            return View(task);
        }



    }
}
