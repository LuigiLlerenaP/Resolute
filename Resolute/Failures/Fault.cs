using Resolute.Contracts;

namespace Resolute.Failures
{
    /// <summary>
    /// Representa la falla controlada de una operación.
    /// </summary>
    /// <param name="code"> identificador técnico único de la falla</param>
    /// <param name="message">descripción legible</param>
    /// <param name="type">tipo de falla</param>
    public class Fault(string code, string message, FaultType type = FaultType.Validation) : IFault, IEquatable<Fault>
    {
        /// <summary>
        /// Código de error único que identifica la falla (ej. "Order.NotFound").
        /// </summary>
        public string Code { get; } = code ?? string.Empty;

        /// <summary>
        /// Mensaje descriptivo que explica la naturaleza de la falla.
        /// </summary>
        public string Message { get; } = message ?? string.Empty;

        /// <summary>
        /// Tipo de falla que indica la categoría semántica del error.
        /// </summary>
        public FaultType Type { get; } = type;

        /// <summary>
        /// Determina si la instancia actual de <see cref="Fault"/> es igual a otra instancia de <see cref="Fault"/>.
        /// </summary>
        public bool Equals(Fault? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Type == other.Type
                && Validate(Code, other.Code)
                && Validate(Message, other.Message);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => Equals(obj as Fault);

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(
                Type,
                StringComparer.OrdinalIgnoreCase.GetHashCode(Code),
                StringComparer.OrdinalIgnoreCase.GetHashCode(Message));

        /// <summary>
        /// Determina si dos instancias de <see cref="Fault"/> son iguales. Esta sobrecarga del operador == permite comparar directamente dos objetos <see cref="Fault"/> utilizando el operador de igualdad.
        /// </summary>
        /// <param name="left">La primera instancia de <see cref="Fault"/> a comparar.</param>
        /// <param name="right">La segunda instancia de <see cref="Fault"/> a comparar.</param>
        /// <returns>true si las instancias son iguales; de lo contrario, false.</returns>
        public static bool operator ==(Fault? left, Fault? right) =>
            left is null ? right is null : left.Equals(right);

        /// <summary>
        /// Determina si dos instancias de <see cref="Fault"/> no son iguales. Esta sobrecarga del operador != permite comparar directamente dos objetos <see cref="Fault"/> utilizando el operador de desigualdad.
        /// </summary>
        /// <param name="left">La primera instancia de <see cref="Fault"/> a comparar.</param>
        /// <param name="right">La segunda instancia de <see cref="Fault"/> a comparar.</param>
        /// <returns>true si las instancias no son iguales; de lo contrario, false. </returns>
        public static bool operator !=(Fault? left, Fault? right) => !(left == right);

        /// <inheritdoc />
        public override string ToString() => $"[{Type}] [{Code}] {Message}";

        private static bool Validate(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}
