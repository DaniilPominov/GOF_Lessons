namespace Homework4._2
{
    public class MiddlewarePipeline
    {
        private Middleware? _firstMiddleware;

        public void AddMiddleware(Middleware middleware)
        {
            if (_firstMiddleware == null)
            {
                _firstMiddleware = middleware;
            }
            else
            {
                Middleware current = _firstMiddleware;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.SetNext(middleware);
            }
        }

        public void Execute(Request request)
        {
            request.IsValid = true;
            _firstMiddleware?.Process(request);

            if (!request.IsValid)
            {
                Console.WriteLine("Request rejected");
            }
            else
            {
                Console.WriteLine("Request completed successfully");
            }
            Console.WriteLine();
        }
    }
}
