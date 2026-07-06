namespace Resolute.Failures
{
    /// <summary>
    ///Excepción que se produce cuando un <c>Result</c> se crea o se utiliza de forma inválida
    /// </summary>
    public sealed class InvalidResultException : Exception
    {
        /// <summary>
        /// The state of the result that triggered the invalid operation (for diagnostics).
        /// </summary>
        public string? ResultState { get; }

        /// <summary>
        /// The operation or method name where the invalid result was detected.
        /// </summary>
        public string? Operation { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InvalidResultException()
        {
        }

        /// <summary>
        /// Creates the exception with a custom message.
        /// </summary>
        public InvalidResultException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates the exception with an inner exception.
        /// </summary>
        public InvalidResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates the exception including contextual information about the result state and the operation.
        /// </summary>
        public InvalidResultException(
            string resultState,
            string operation,
            string message)
            : base(message)
        {
            ResultState = resultState;
            Operation = operation;
        }
    }
}
