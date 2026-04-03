namespace Homework4._2
{
    public class RequestHandlerMiddleware : Middleware
    {
        public override void Process(Request request)
        {
            Console.WriteLine($"Processing: {request.Operation} with data '{request.Data}'");
        }
    }
}
