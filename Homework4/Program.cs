namespace Homework4
{
    internal class Program
    {
        static void Main(string[] args) { 

            INotificationService notificationService = new ExternalNotificationService();

            var order = new Order("ORD-001", "Создан");

            var emailObserver = new EmailObserver("client@example.com", notificationService);
            var smsObserver = new SmsObserver("+7 (999) 123-45-67", notificationService);
            var pushObserver = new PushObserver("user123", notificationService);
            var loggingObserver = new LoggingObserver();

            
            order.Subscribe(emailObserver);
            order.Subscribe(smsObserver);
            order.Subscribe(pushObserver);
            order.Subscribe(loggingObserver);

            Console.WriteLine("Статус в обработку");
            order.Status = "Обработка";
            Console.WriteLine("Статус в отправлено");
            order.Status = "Отправлено";

            Console.WriteLine("Отписываем Email");
            order.Unsubscribe(emailObserver);

            Console.WriteLine("Статус в доставлено");
            order.Status = "Доставлено";

            var emailObserver2 = new EmailObserver("manager@example.com", notificationService);
            Console.WriteLine("Новый email подписчик");
            order.Subscribe(emailObserver2);

            Console.WriteLine("Статус завершено");
            order.Status = "Завершено";
        }
    }
}
