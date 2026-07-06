namespace Resolute.Common.ProblemsConst
{
    /// <summary>
    /// Machine friendly fault codes used to identify fault types programmatically.
    /// Many codes are templates where {0} is replaced with a property or entity name.
    /// </summary>
    public static class FaultCodes
    {

        /// <summary>Template: {0}.Required</summary>
        public const string Required = "{0}.Required";

        /// <summary>Template: {0}.Empty</summary>
        public const string Empty = "{0}.Empty";

        /// <summary>Template: {0}.Invalid</summary>
        public const string Invalid = "{0}.Invalid";

        /// <summary>Template: {0}.InvalidFormat</summary>
        public const string InvalidFormat = "{0}.InvalidFormat";

        /// <summary>Template: {0}.Negative</summary>
        public const string Negative = "{0}.Negative";

        /// <summary>Template: {0}.OutOfRange</summary>
        public const string OutOfRange = "{0}.OutOfRange";

        /// <summary>Template: {0}.TooLong</summary>
        public const string TooLong = "{0}.TooLong";

        /// <summary>Template: {0}.NotFound</summary>
        public const string NotFound = "{0}.NotFound";

        /// <summary>Generic conflict code.</summary>
        public const string Conflict = "Conflict";

        /// <summary>Unauthorized code.</summary>
        public const string Unauthorized = "Unauthorized";

        /// <summary>Unexpected/internal error code.</summary>
        public const string Unexpected = "Unexpected";
    }
}
