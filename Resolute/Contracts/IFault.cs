using Resolute.Failures;

namespace Resolute.Contracts;

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

    /// <summary>
    /// The semantic category of the fault, used to map domain failures to transport-level responses (e.g. HTTP status codes).
    /// </summary>
    FaultType Type { get; }
}
