using Resolute.Common.ProblemsConst;
using Resolute.Failures;

namespace Resolute.Common.Problems
{
    /// <summary>
    /// Factory helpers to create common <see cref="Resolute.Failures.Fault"/> instances used across the application.
    /// </summary>
    public static class Faults
    {
        /// <summary>
        /// Creates a fault indicating a required property is missing or null.
        /// </summary>
        public static Fault Required(string propertyName)
            => new(string.Format(FaultCodes.Required, propertyName),
                   string.Format(FaultMessages.Required, propertyName));

        /// <summary>
        /// Creates a fault indicating a property is empty.
        /// </summary>
        public static Fault Empty(string propertyName)
            => new(string.Format(FaultCodes.Empty, propertyName),
                   string.Format(FaultMessages.Empty, propertyName));

        /// <summary>
        /// Creates a fault indicating a property has an invalid value.
        /// </summary>
        public static Fault Invalid<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.Invalid, propertyName),
                   string.Format(FaultMessages.Invalid, value));

        /// <summary>
        /// Creates a fault indicating a property has an invalid format.
        /// </summary>
        public static Fault InvalidFormat<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.InvalidFormat, propertyName),
                   string.Format(FaultMessages.InvalidFormat, value));

        /// <summary>
        /// Creates a fault indicating a numeric property must be non-negative.
        /// </summary>
        public static Fault MustBeNonNegative<T>(string propertyName, T value)
            => new(string.Format(FaultCodes.Negative, propertyName),
                   string.Format(FaultMessages.MustBeNonNegative, value));

        /// <summary>
        /// Creates a fault indicating a value is outside the allowed range.
        /// </summary>
        public static Fault OutOfRange<T>(string propertyName, T value , int min, int max)
            => new(string.Format(FaultCodes.OutOfRange, propertyName),
                   string.Format(FaultMessages.OutOfRange, value, min, max));

        /// <summary>
        /// Creates a fault indicating a string has exceeded the maximum permitted length.
        /// </summary>
        public static Fault MaxLengthExceeded(string propertyName, int maxLength)
            => new(string.Format(FaultCodes.TooLong, propertyName),
                   string.Format(FaultMessages.MaxLengthExceeded, propertyName, maxLength));

        /// <summary>
        /// Creates a fault indicating an entity with the given key was not found.
        /// </summary>
        public static Fault NotFound<TKey>(string entityName, TKey key)
            => new(string.Format(FaultCodes.NotFound, entityName),
                   string.Format(FaultMessages.NotFound, entityName, key));

        /// <summary>
        /// Creates a generic conflict fault with the provided message.
        /// </summary>
        public static Fault Conflict(string message)
            => new(FaultCodes.Conflict, message);

        /// <summary>
        /// Creates an unauthorized fault.
        /// </summary>
        public static Fault Unauthorized()
            => new(FaultCodes.Unauthorized,
                   FaultMessages.Unauthorized);

        /// <summary>
        /// Creates an unexpected/internal fault with an optional custom message.
        /// </summary>
        public static Fault Unexpected(string message = FaultMessages.Unexpected)
            => new(FaultCodes.Unexpected, message);
    }
}
