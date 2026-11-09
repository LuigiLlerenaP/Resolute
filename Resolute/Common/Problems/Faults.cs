using Resolute.Common.ProblemsConst;
using Resolute.Failures;

namespace Resolute.Common.Problems
{
    /// <summary>
    /// Factory helpers to create common <see cref="Fault"/> instances used across the application.
    /// </summary>
    public static class Faults
    {
        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a required property is missing or null.
        /// </summary>
        public static Fault Required(string propertyName)
            => new(string.Format(FaultCodes.Required, propertyName),
                   string.Format(FaultMessages.Required, propertyName),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a property is empty.
        /// </summary>
        public static Fault Empty(string propertyName)
            => new(string.Format(FaultCodes.Empty, propertyName),
                   string.Format(FaultMessages.Empty, propertyName),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a property has an invalid value.
        /// </summary>
        public static Fault Invalid<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.Invalid, propertyName),
                   string.Format(FaultMessages.Invalid, value),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a property has an invalid format.
        /// </summary>
        public static Fault InvalidFormat<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.InvalidFormat, propertyName),
                   string.Format(FaultMessages.InvalidFormat, value),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a numeric property must be non-negative.
        /// </summary>
        public static Fault MustBeNonNegative<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.Negative, propertyName),
                   string.Format(FaultMessages.MustBeNonNegative, value),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a value is outside the allowed range.
        /// </summary>
        public static Fault OutOfRange<T>(string propertyName, T value, int min, int max)
            => new(string.Format(FaultCodes.OutOfRange, propertyName),
                   string.Format(FaultMessages.OutOfRange, value, min, max),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.Validation"/> fault indicating a string has exceeded the maximum permitted length.
        /// </summary>
        public static Fault MaxLengthExceeded(string propertyName, int maxLength)
            => new(string.Format(FaultCodes.TooLong, propertyName),
                   string.Format(FaultMessages.MaxLengthExceeded, propertyName, maxLength),
                   FaultType.Validation);

        /// <summary>
        /// Creates a <see cref="FaultType.NotFound"/> fault indicating that an entity with the specified key was not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the entity identifier.</typeparam>
        /// <param name="entityName">The name of the entity that was not found.</param>
        /// <param name="key">The identifier value used in the lookup.</param>
        /// <returns>A <see cref="Fault"/> categorized as <see cref="FaultType.NotFound"/>.</returns>
        public static Fault NotFound<TKey>(string entityName, TKey key)
            => new(string.Format(FaultCodes.NotFound, entityName),
                   string.Format(FaultMessages.NotFound, entityName, key),
                   FaultType.NotFound);

        /// <summary>
        /// Creates a <see cref="FaultType.Conflict"/> fault with the specified message (e.g., duplicate key or invalid state transition).
        /// </summary>
        /// <param name="message">A descriptive message explaining the conflict.</param>
        /// <returns>A <see cref="Fault"/> categorized as <see cref="FaultType.Conflict"/>.</returns>
        public static Fault Conflict(string message)
            => new(FaultCodes.Conflict, message, FaultType.Conflict);

        /// <summary>
        /// Creates an <see cref="FaultType.Unauthorized"/> fault indicating that authentication is required or has failed.
        /// </summary>
        /// <returns>A <see cref="Fault"/> categorized as <see cref="FaultType.Unauthorized"/>.</returns>
        public static Fault Unauthorized()
            => new(FaultCodes.Unauthorized,
                   FaultMessages.Unauthorized,
                   FaultType.Unauthorized);

        /// <summary>
        /// Creates a <see cref="FaultType.Forbidden"/> fault indicating that the caller lacks permission to perform the operation.
        /// </summary>
        /// <param name="message">An optional custom message explaining the permission denial.</param>
        /// <returns>A <see cref="Fault"/> categorized as <see cref="FaultType.Forbidden"/>.</returns>
        public static Fault Forbidden(string message = "You do not have permission to perform this action.")
            => new("General.Forbidden", message, FaultType.Forbidden);

        /// <summary>
        /// Creates an <see cref="FaultType.Unexpected"/> fault representing an unhandled or internal error.
        /// </summary>
        /// <param name="message">An optional custom message describing the unexpected error.</param>
        /// <returns>A <see cref="Fault"/> categorized as <see cref="FaultType.Unexpected"/>.</returns>
        public static Fault Unexpected(string message = FaultMessages.Unexpected)
            => new(FaultCodes.Unexpected, message, FaultType.Unexpected);
    }
}
