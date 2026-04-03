namespace Homework4._2
{
    public class AuthorizationMiddleware : Middleware
    {
        private readonly Dictionary<string, List<string>> _permissions = new()
        {
            { "admin", new List<string> { "read", "write", "delete" } },
            { "user", new List<string> { "read", "write" } },
            { "guest", new List<string> { "read" } }
        };

        public override void Process(Request request)
        {
            if (!_permissions.ContainsKey(request.User) ||
                !_permissions[request.User].Contains(request.Operation))
            {
                Console.WriteLine($"Authorization failed: {request.User} cannot {request.Operation}");
                request.IsValid = false;
                return;
            }

            Console.WriteLine("Authorization passed");
            _next?.Process(request);
        }
    }
}
