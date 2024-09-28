using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TManagement.Entities;

namespace TManagement.Views.Shared.Components.Notifications
{
    public class NotificationsViewComponent(AppDbContext appDbContext): ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync(int maxRecords=10)
        {
            var userEmail = User.Identity.Name;

            var unReadNotifications = await appDbContext.SystemNotifications.Where(s => s.AppUser.Email == userEmail && s.ReadDate.HasValue == false).CountAsync();

            var  data =await appDbContext.SystemNotifications.Where(s=>s.AppUser.Email== userEmail)
                .OrderByDescending(s=>s.NotificationDate)
                .Take(maxRecords).ToListAsync();


            var model = new ViewNotificationModel() { Notifications = data, UnreadCount = unReadNotifications };

            return View(model);


        }

    }
}
