using TManagement.Entities;

namespace TManagement.Views.Shared.Components.Notifications
{
    public class ViewNotificationModel
    {
        public int UnreadCount { get; set; }    

        public List<SystemNotification> Notifications { get; set; }
    }
}
