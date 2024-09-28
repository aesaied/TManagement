using Microsoft.AspNetCore.SignalR;

namespace TManagement.Hubs
{
    public class NotificationHub : Hub
    {

        public static List<string>  NotAllOwedUsersConnections= new List<string>();

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.Name == "ala@yahoo.com") {
                NotAllOwedUsersConnections.Add(Context.ConnectionId);
            }
         
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (NotAllOwedUsersConnections.Contains(Context.ConnectionId))
            {
                NotAllOwedUsersConnections.Remove(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

        //
        public async Task SendNotification(string msg)
        {
           await this.Clients.AllExcept(NotAllOwedUsersConnections).SendAsync("receiveNotification",msg);

            //this.Clients.Caller.SendAsync
        }
    }
}
