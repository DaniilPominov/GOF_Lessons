using System;
using System.Collections.Generic;

namespace Homework4
{
    internal interface INotificationCommand
    {
        void Execute();
    }


    internal class EmailNotificationCommand : INotificationCommand
    {
        private readonly string _recipient;
        private readonly string _message;
        private readonly INotificationService _service;

        public EmailNotificationCommand(string recipient, string message, INotificationService service)
        {
            _recipient = recipient;
            _message = message;
            _service = service;
        }

        public void Execute()
        {
            _service.SendEmail(_recipient, _message);
        }
    }

    internal class SmsNotificationCommand : INotificationCommand
    {
        private readonly string _phoneNumber;
        private readonly string _message;
        private readonly INotificationService _service;

        public SmsNotificationCommand(string phoneNumber, string message, INotificationService service)
        {
            _phoneNumber = phoneNumber;
            _message = message;
            _service = service;
        }

        public void Execute()
        {
            _service.SendSms(_phoneNumber, _message);
        }
    }
    internal class PushNotificationCommand : INotificationCommand
    {
        private readonly string _userId;
        private readonly string _title;
        private readonly string _message;
        private readonly INotificationService _service;

        public PushNotificationCommand(string userId, string title, string message, INotificationService service)
        {
            _userId = userId;
            _title = title;
            _message = message;
            _service = service;
        }

        public void Execute()
        {
            _service.SendPush(_userId, _title, _message);
        }
    }

    internal interface INotificationService
    {
        void SendEmail(string recipient, string message);
        void SendSms(string phoneNumber, string message);
        void SendPush(string userId, string title, string message);
    }

    internal class ExternalNotificationService : INotificationService
    {
        public void SendEmail(string recipient, string message)
        {
            Console.WriteLine($"Email отправлен на {recipient}");
            Console.WriteLine($"Сообщение: {message}\n");
        }

        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"SMS отправлено на {phoneNumber}");
            Console.WriteLine($"Сообщение: {message}\n");
        }

        public void SendPush(string userId, string title, string message)
        {
            Console.WriteLine($"Push-уведомление пользователю {userId}");
            Console.WriteLine($"Заголовок: {title}");
            Console.WriteLine($"Текст: {message}\n");
        }
    }

    internal interface IStatusObserver
    {
        void Update(StatusChangedEventArgs eventArgs);
    }

    internal class StatusChangedEventArgs
    {
        public string OrderId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
    }

    internal class EmailObserver : IStatusObserver
    {
        private readonly string _email;
        private readonly INotificationService _notificationService;

        public EmailObserver(string email, INotificationService notificationService)
        {
            _email = email;
            _notificationService = notificationService;
        }

        public void Update(StatusChangedEventArgs eventArgs)
        {
            var message = $"Заказ {eventArgs.OrderId} изменил статус с '{eventArgs.OldStatus}' на '{eventArgs.NewStatus}'";
            var command = new EmailNotificationCommand(_email, message, _notificationService);
            command.Execute();
        }
    }

    internal class SmsObserver : IStatusObserver
    {
        private readonly string _phoneNumber;
        private readonly INotificationService _notificationService;

        public SmsObserver(string phoneNumber, INotificationService notificationService)
        {
            _phoneNumber = phoneNumber;
            _notificationService = notificationService;
        }

        public void Update(StatusChangedEventArgs eventArgs)
        {
            var message = $"Заказ {eventArgs.OrderId}: {eventArgs.OldStatus} → {eventArgs.NewStatus}";
            var command = new SmsNotificationCommand(_phoneNumber, message, _notificationService);
            command.Execute();
        }
    }

    internal class PushObserver : IStatusObserver
    {
        private readonly string _userId;
        private readonly INotificationService _notificationService;

        public PushObserver(string userId, INotificationService notificationService)
        {
            _userId = userId;
            _notificationService = notificationService;
        }

        public void Update(StatusChangedEventArgs eventArgs)
        {
            var title = "Обновление статуса заказа";
            var message = $"Заказ {eventArgs.OrderId}: {eventArgs.NewStatus}";
            var command = new PushNotificationCommand(_userId, title, message, _notificationService);
            command.Execute();
        }
    }

    internal class LoggingObserver : IStatusObserver
    {
        public void Update(StatusChangedEventArgs eventArgs)
        {
            Console.WriteLine($"[log] {eventArgs.ChangedAt:yyyy-MM-dd HH:mm:ss} - " +
                $"Заказ {eventArgs.OrderId}: {eventArgs.OldStatus} -> {eventArgs.NewStatus}");
        }
    }

    internal class Order
    {
        private string _status;
        private readonly List<IStatusObserver> _observers = new();

        public string OrderId { get; }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    var oldStatus = _status;
                    _status = value;
                    NotifyObservers(oldStatus);
                }
            }
        }

        public Order(string orderId, string initialStatus = "Создан")
        {
            OrderId = orderId;
            _status = initialStatus;
        }
        public void Subscribe(IStatusObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"Подписчик добавлен. Всего подписчиков: {_observers.Count}\n");
            }
        }

        public void Unsubscribe(IStatusObserver observer)
        {
            if (_observers.Remove(observer))
            {
                Console.WriteLine($"Подписчик удален. Остались подписчиков: {_observers.Count}\n");
            }
        }

        private void NotifyObservers(string oldStatus)
        {
            Console.WriteLine($"\nУведомление подписчикам об изменении статуса заказа {OrderId}:");
            Console.WriteLine(new string('=', 60));

            var eventArgs = new StatusChangedEventArgs
            {
                OrderId = OrderId,
                OldStatus = oldStatus,
                NewStatus = _status,
                ChangedAt = DateTime.Now
            };

            foreach (var observer in _observers)
            {
                observer.Update(eventArgs);
            }

            Console.WriteLine(new string('=', 60));
        }
    }
}
