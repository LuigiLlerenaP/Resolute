namespace Resolute.Faults
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
        public int StatusCode { get; } = statusCode;
        public bool Equals(HttpFault? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return StatusCode == other.StatusCode && base.Equals(other);
        }

        public override bool Equals(object? obj) => Equals(obj as HttpFault);

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), StatusCode);

        public static bool operator ==(HttpFault? left, HttpFault? right) =>
            left is null ? right is null : left.Equals(right);

        public static bool operator !=(HttpFault? left, HttpFault? right) => !(left == right);
        public override string ToString() => $"[{StatusCode}] {base.ToString()}";
    }
}
