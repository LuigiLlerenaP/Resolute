namespace Resolute.Failures
{
    /// <summary>
    /// Representa una falla controlada de una operación que está específicamente asociada a un código de estado HTTP.
    /// Hereda de <see cref="Fault"/> para integrarse con el manejo general de errores del dominio.
    /// </summary>
    /// <param name="statusCode">El código de estado HTTP (ej. 400, 404, 500).</param>
    /// <param name="code">Identificador técnico único de la falla.</param>
    /// <param name="message">Descripción legible del error.</param>
    public class HttpFault(int statusCode, string code, string message) : Fault(code, message), IEquatable<HttpFault>
    {
        /// <summary>
        /// HTTP status code associated to the fault (e.g. 400, 404, 500).
        /// </summary>
        public int StatusCode { get; } = statusCode;

        /// <summary>
        /// Compares two <see cref="HttpFault"/> instances for equality.
        /// </summary>
        /// <param name="other">Other HttpFault to compare with.</param>
        public bool Equals(HttpFault? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return StatusCode == other.StatusCode && base.Equals(other);
        }

        /// <summary>
        /// Overrides object equality.
        /// </summary>
        public override bool Equals(object? obj) => Equals(obj as HttpFault);

        /// <summary>
        /// Computes hash code for the instance.
        /// </summary>
        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), StatusCode);

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(HttpFault? left, HttpFault? right) =>
            left is null ? right is null : left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(HttpFault? left, HttpFault? right) => !(left == right);

        /// <summary>
        /// Returns a friendly string representation including status code and fault.
        /// </summary>
        public override string ToString() => $"[{StatusCode}] {base.ToString()}";
    }
}
