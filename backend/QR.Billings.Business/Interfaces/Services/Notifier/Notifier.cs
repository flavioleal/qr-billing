using QR.Billings.Business.Interfaces.Notifier;

namespace QR.Billings.Business.Interfaces.Services.Notifier
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }

    }
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
