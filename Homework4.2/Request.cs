namespace Homework4._2
{
    public class Request
    {
        public string User { get; set; } = "";
        public string Token { get; set; } = "";
        public string Operation { get; set; } = "";
        public string Data { get; set; } = "";
        public bool IsValid { get; set; }
    }
}
