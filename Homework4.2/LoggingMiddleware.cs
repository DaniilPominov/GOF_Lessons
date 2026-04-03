namespace Homework4._2
{
    public class LoggingMiddleware : Middleware
    {
        public override void Process(Request request)
        {
            Console.WriteLine($"Logging: User={request.User}, Operation={request.Operation}");
            _next?.Process(request);
        }
    }
}
