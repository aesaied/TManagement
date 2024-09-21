using Microsoft.AspNetCore.Mvc;

namespace TManagement.Controllers
{
    public static class ControllerExtensions
    {

        public static void SetMessage(this  Controller controller, string msg)
        {
           controller.TempData["Message"] = msg;

        }
    }
}
