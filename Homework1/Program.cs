using System.Text;

namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Sb = new StringBuilder();
            Sb.Append("Hello, World!");
            Console.WriteLine("Hello, World!");
        }
    }
    //пример паттерна Builder во встроенных классах .NET

    public class  Example
    {
        public void BuildString()
        {
            // StringBuilder реализует паттерн Builder,
            // позволяя создавать сложные строки поэтапно
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hello, ");
            stringBuilder.Append("World!");
            string result = stringBuilder.ToString();
            Console.WriteLine(result);
        }

    }



    //пример нарушения srp
    //класс User описывает модель пользователя и выполняет функции
    //по сохранению данных и отправке email, что нарушает принцип единственной ответственности
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public void AssignUserRole(string role)
        {
            Role = role;
        }
        public void SaveToDatabase()
        {

        }
        public void SendEmail(string message)
        {

        }
    }
    public class WrongUserService
    {
        public void CreateUser(string name, string email)
        {
            User user = new User { Name = name, Email = email };
            user.SaveToDatabase();
            user.SendEmail("Welcome to our service!");
        }
        public void AssignRole(User user, string role)
        {
            user.AssignUserRole(role);
            user.SaveToDatabase();
        }
    }
    //пример соблюдения srp
    public class UserRepository
    {
        public void Save(User user)
        {
        }
    }
    public class EmailService
    {
        public void SendEmail(string email, string message)
        {
        }
    }

    //класс UserService отвечает только за создание пользователя
    //и делегирует функции сохранения данных и отправки email другим классам
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly EmailService _emailService;
        public UserService(UserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public void CreateUser(string name, string email)
        {
            User user = new User { Name = name, Email = email };
            _userRepository.Save(user);
            _emailService.SendEmail(email, "Welcome to our service!");
        }
    }
}
