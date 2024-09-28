namespace TManagement.Services
{
    public class NotificationInfo
    {
        public required string Message { get; set; }

        public NotificationType Type { get; set; } = NotificationType.Info;

    }

    public enum NotificationType { 
      Success,
      Error, 
      Info,
      Warning
    }


}
