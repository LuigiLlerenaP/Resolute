namespace Resolute.Common.ProblemsConst
{
    /// <summary>
    /// Standard fault message templates used to create human readable fault messages.
    /// Placeholders use string.Format semantics (e.g. {0}, {1}).
    /// </summary>
    public static class FaultMessages
    {
        /// <summary>The '{0}' field is required.</summary>
        public const string Required = "The '{0}' field is required.";

        /// <summary>The '{0}' field cannot be empty.</summary>
        public const string Empty = "The '{0}' field cannot be empty.";

        /// <summary>The '{0}' field is invalid.</summary>
        public const string Invalid = "The '{0}' field is invalid.";

        /// <summary>The '{0}' field has an invalid format.</summary>
        public const string InvalidFormat = "The '{0}' field has an invalid format.";

        /// <summary>The '{0}' field cannot be negative.</summary>
        public const string MustBeNonNegative = "The '{0}' field cannot be negative.";

        /// <summary>Template: The '{0}' field must be between {1} and {2}.</summary>
        public const string OutOfRange = "The '{0}' field must be between {1} and {2}.";

        /// <summary>Template: The '{0}' field must not exceed {1} characters.</summary>
        public const string MaxLengthExceeded = "The '{0}' field must not exceed {1} characters.";

        /// <summary>Template: The '{0}' with identifier '{1}' was not found.</summary>
        public const string NotFound = "The '{0}' with identifier '{1}' was not found.";

        /// <summary>You are not authorized to perform this operation.</summary>
        public const string Unauthorized = "You are not authorized to perform this operation.";

        /// <summary>An unexpected Fault occurred.</summary>
        public const string Unexpected = "An unexpected Fault occurred.";
    }
}
