namespace Resolute.Contracts
{
    /// <summary>
    /// Represents a minimal fault contract containing a machine-friendly code and a human readable message.
    /// </summary>
    public interface IFault
    {
        /// <summary>
        /// A short machine-readable identifier for the fault (e.g. "not_found").
        /// </summary>
        string Code { get; }

        /// <summary>
        /// A human readable description of the fault suitable for logs or user messages.
        /// </summary>
        string Message { get; }
    }
}
