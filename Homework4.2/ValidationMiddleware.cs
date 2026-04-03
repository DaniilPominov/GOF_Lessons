namespace Homework4._2
{
    public class ValidationMiddleware : Middleware
    {
        public override void Process(Request request)
        {
            if (string.IsNullOrEmpty(request.Data) || request.Data.Length < 3)
            {
                Console.WriteLine("Validation failed: data must be at least 3 characters");
                request.IsValid = false;
                return;
            }

            Console.WriteLine("Validation passed");
            _next?.Process(request);
        }
    }
}
