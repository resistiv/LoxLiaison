namespace LoxLiaison.Exceptions
{
    /// <summary>
    /// Represents a return statement.
    /// </summary>
    public class ReturnException : RuntimeException
    {
        /// <summary>
        /// The returned value.
        /// </summary>
        public readonly object Value;

        public ReturnException(object value)
            : base(null, null)
        {
            Value = value;
        }
    }
}
