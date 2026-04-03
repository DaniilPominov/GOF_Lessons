namespace Homework4._2
{
    public abstract class Middleware
    {
        protected Middleware? _next;
        public Middleware? Next
        {
            get { return _next; }
        }

        public void SetNext(Middleware next)
        {
            _next = next;
        }

        public abstract void Process(Request request);
    }
}
