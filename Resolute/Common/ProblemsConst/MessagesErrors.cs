namespace Resolute.Common.ProblemsConst
{
    /// <summary>
    /// Standard error messages for HTTP-based operations.
    /// </summary>
    public static class MessagesErrors
    {
        // 4xx Client Errors
        /// <summary>The request is invalid or poorly formatted.</summary>
        public const string BadRequest = "The request is invalid or poorly formatted.";

        /// <summary>Authentication is required to access this resource.</summary>
        public const string Unauthorized = "Authentication is required to access this resource.";

        /// <summary>You do not have permission to access this resource.</summary>
        public const string Forbidden = "You do not have permission to access this resource.";

        /// <summary>The requested resource could not be found.</summary>
        public const string NotFound = "The requested resource could not be found.";

        /// <summary>The HTTP method used is not allowed for this resource.</summary>
        public const string MethodNotAllowed = "The HTTP method used is not allowed for this resource.";

        /// <summary>There is a conflict with the current state of the resource.</summary>
        public const string Conflict = "There is a conflict with the current state of the resource.";

        /// <summary>The request was well-formed but contains semantic errors (e.g., validation failed).</summary>
        public const string UnprocessableEntity = "The request was well-formed but contains semantic errors (e.g., validation failed).";

        // 5xx Server Errors
        /// <summary>An unexpected error occurred on the server.</summary>
        public const string InternalServerError = "An unexpected error occurred on the server.";

        /// <summary>The server does not support the functionality required to fulfill the request.</summary>
        public const string NotImplemented = "The server does not support the functionality required to fulfill the request.";

        /// <summary>The server received an invalid response from an upstream server.</summary>
        public const string BadGateway = "The server received an invalid response from an upstream server.";

        /// <summary>The server is currently unable to handle the request due to temporary overloading or maintenance.</summary>
        public const string ServiceUnavailable = "The server is currently unable to handle the request due to temporary overloading or maintenance.";
    }
}
