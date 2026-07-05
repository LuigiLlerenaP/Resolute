namespace Resolute.Common.ProblemsConst
{
    public static class FaultMessages
    {
        public const string Required = "The '{0}' field is required.";
        public const string Empty = "The '{0}' field cannot be empty.";
        public const string Invalid = "The '{0}' field is invalid.";
        public const string InvalidFormat = "The '{0}' field has an invalid format.";
        public const string MustBeNonNegative = "The '{0}' field cannot be negative.";

        // Plantilla de 3 parámetros ({0} = propertyName, {1} = min, {2} = max)
        public const string OutOfRange = "The '{0}' field must be between {1} and {2}.";

        // Plantilla de 2 parámetros ({0} = propertyName, {1} = maxLength)
        public const string MaxLengthExceeded = "The '{0}' field must not exceed {1} characters.";

        // Plantilla de 2 parámetros ({0} = entityName, {1} = key)
        public const string NotFound = "The '{0}' with identifier '{1}' was not found.";

        public const string Unauthorized = "You are not authorized to perform this operation.";
        public const string Unexpected = "An unexpected Fault occurred.";
    }
}
