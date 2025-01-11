namespace NewsItems.Exception
{
    public class ExceptionNewsItemExists : System.Exception
    {
        public ExceptionNewsItemExists() { }
        public ExceptionNewsItemExists(string message) : base(message) { }
    }
}
