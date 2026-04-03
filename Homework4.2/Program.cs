namespace Homework4._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pipeline = new MiddlewarePipeline();
            pipeline.AddMiddleware(new AuthenticationMiddleware());
            pipeline.AddMiddleware(new AuthorizationMiddleware());
            pipeline.AddMiddleware(new ValidationMiddleware());
            pipeline.AddMiddleware(new LoggingMiddleware());
            pipeline.AddMiddleware(new RequestHandlerMiddleware());

            var request1 = new Request
            {
                User = "admin",
                Token = "valid_token",
                Operation = "delete",
                Data = "important data"
            };
            pipeline.Execute(request1);

            var request2 = new Request
            {
                User = "guest",
                Token = "valid_token",
                Operation = "write",
                Data = "some data"
            };
            pipeline.Execute(request2);

            var request3 = new Request
            {
                User = "user",
                Token = "",
                Operation = "read",
                Data = "test"
            };
            pipeline.Execute(request3);

            var request4 = new Request
            {
                User = "user",
                Token = "valid_token",
                Operation = "read",
                Data = "ok"
            };
            pipeline.Execute(request4);
        }
    }
}
