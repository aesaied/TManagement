using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TManagement.AppServices.Account;
using TManagement.Entities;
using TManagement.Hubs;

namespace TManagement.Services
{
    public class NotificationManager(IHubContext<NotificationHub> _hub, AppDbContext dbContext) : INotificationManager
    {

        public async Task NotifyWithoutDb(NotificationInfo msg)
        {
            await _hub.Clients.All.SendAsync("receiveNotification", msg);

        }

        public async Task Notify(string group,  NotificationInfo msg)
        {

            var users =await dbContext.UserGroups.Where(s => s.Group.Name == group).Select(s=>new { UserId= s.UserId, Email= s.User.Email}).ToListAsync();


            var notications = users.Select(user => new SystemNotification() {  Message=msg.Message, NotificationDate=DateTime.Now, NotificationType=msg.Type, ReadDate=null, UserId=Convert.ToInt32( user.UserId), Id=Guid.NewGuid()});

            dbContext.SystemNotifications.AddRange(notications);

            await dbContext.SaveChangesAsync();
            //foreach (var userInfo in users)
            //{
            //    var user = _hub.Clients.User(userInfo.UserId.ToString());

            //    if (user != null)
            //    {
            //        await user.SendAsync("receiveNotification", msg);
            //    }
            //}
           await _hub.Clients.All.SendAsync("receiveNotification", msg);

        }




    }
}
