
namespace TManagement.Services
{
    public interface INotificationManager
    {
        Task Notify(string group, NotificationInfo msg);
        Task NotifyWithoutDb(NotificationInfo msg);
    }
}