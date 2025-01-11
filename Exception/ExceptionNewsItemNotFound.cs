namespace NewsItems.Exception
{
    public class ExceptionNewsItemNotFound : System.Exception
    {
        public ExceptionNewsItemNotFound() : base() { }
        public ExceptionNewsItemNotFound(string message) : base(message) { }
    }
}
