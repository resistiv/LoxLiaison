namespace LoxLiaison.Exceptions
{
    public class ReturnException : RuntimeException
    {
        public readonly object Value;

        public ReturnException(object value)
            : base(null, null)
        {
            Value = value;
        }
    }
}
