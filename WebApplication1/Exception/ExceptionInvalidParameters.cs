namespace NewsItems.Exception
{
    public class ExceptionInvalidParameters : System.Exception
    {
        public ExceptionInvalidParameters() { }
        public ExceptionInvalidParameters(string message) : base(message) { }
    }
}
