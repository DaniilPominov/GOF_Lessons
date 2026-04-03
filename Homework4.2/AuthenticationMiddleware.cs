namespace Homework4._2
{
    public class AuthenticationMiddleware : Middleware
    {
        public override void Process(Request request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                Console.WriteLine("Authentication failed: no token");
                request.IsValid = false;
                return;
            }

            Console.WriteLine("Authentication passed");
            _next?.Process(request);
        }
    }
}
