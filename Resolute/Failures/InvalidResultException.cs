namespace Resolute.Failure
{
    /// <summary>
    ///Excepción que se produce cuando un <c>Result</c> se crea o se utiliza de forma inválida
    /// </summary>
    public sealed class InvalidResultException : Exception
    {
        public string? ResultState { get; }

        public string? Operation { get; }

        public InvalidResultException()
        {
        }

        public InvalidResultException(string message)
            : base(message)
        {
        }

        public InvalidResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

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
