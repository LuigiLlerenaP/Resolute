using Resolute.Common.ProblemsConst;
using Resolute.Faults;

namespace Resolute.Common.Problems
{
    public static class Faults
    {
        public static Fault Required(string propertyName)
            => new(string.Format(FaultCodes.Required, propertyName),
                   string.Format(FaultMessages.Required, propertyName));

        public static Fault Empty(string propertyName)
            => new(string.Format(FaultCodes.Empty, propertyName),
                   string.Format(FaultMessages.Empty, propertyName));

        public static Fault Invalid(string propertyName)
            => new(string.Format(FaultCodes.Invalid, propertyName),
                   string.Format(FaultMessages.Invalid, propertyName));

        public static Fault InvalidFormat(string propertyName)
            => new(string.Format(FaultCodes.InvalidFormat, propertyName),
                   string.Format(FaultMessages.InvalidFormat, propertyName));

        public static Fault MustBeNonNegative(string propertyName)
            => new(string.Format(FaultCodes.Negative, propertyName),
                   string.Format(FaultMessages.MustBeNonNegative, propertyName));

        public static Fault OutOfRange(string propertyName, object min, object max)
            => new(string.Format(FaultCodes.OutOfRange, propertyName),
                   string.Format(FaultMessages.OutOfRange, propertyName, min, max));

        public static Fault MaxLengthExceeded(string propertyName, int maxLength)
            => new(string.Format(FaultCodes.TooLong, propertyName),
                   string.Format(FaultMessages.MaxLengthExceeded, propertyName, maxLength));

        public static Fault NotFound(string entityName, object key)
            => new(string.Format(FaultCodes.NotFound, entityName),
                   string.Format(FaultMessages.NotFound, entityName, key));

        public static Fault Conflict(string message)
            => new(FaultCodes.Conflict, message);

        public static Fault Unauthorized()
            => new(FaultCodes.Unauthorized,
                   FaultMessages.Unauthorized);

        public static Fault Unexpected(string message = FaultMessages.Unexpected)
            => new(FaultCodes.Unexpected, message);
    }
}
