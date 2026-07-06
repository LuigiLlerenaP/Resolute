using Resolute.Contracts;

namespace Resolute.Failures
{
    /// <summary>
    /// Representa la falla controlada de una operación.
    /// </summary>
    /// <param name="code"> identificador técnico único de la falla</param>
    /// <param name="message">descripción legible</param>
    public class Fault (string code, string message) : IFault, IEquatable<Fault> 
    {
        /// <summary>
        ///  Codgio de error único que identifica la falla. Se recomienda usar un formato de nombres jerárquico, como "Module.Submodule.ErrorType", para facilitar la categorización y búsqueda de errores.
        /// </summary>
        public string Code { get; } = code;

        /// <summary>
        /// Mensaje descriptivo que explica la naturaleza de la falla. Este mensaje debe ser claro y conciso, proporcionando suficiente información para que los desarrolladores o usuarios puedan entender el problema sin necesidad de consultar documentación adicional. 
        /// </summary>
        public string Message { get; } = message;

        /// <summary>
        ///  Determina si la instancia actual de <see cref="Fault"/> es igual a otra instancia de <see cref="Fault"/>.
        /// </summary>
        /// <param name="other">La instancia de <see cref="Fault"/> a comparar.</param>
        /// <returns>true si las instancias son iguales; de lo contrario, false.</returns>
        public bool Equals(Fault? other)
        {
            if (other is null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return Validate(Code, other.Code) && Validate(Message, other.Message);
        }

        /// <summary>
        ///  Determina si la instancia actual de <see cref="Fault"/> es igual a otro objeto.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns>true si las instancias son iguales; de lo contrario, false.</returns>
        public override bool Equals(object? obj)
            => Equals(obj as Fault);

        /// <summary>
        /// Calcula un código hash para la instancia actual de <see cref="Fault"/>. Este código hash se basa en el código y el mensaje de la falla, y es útil para estructuras de datos que dependen de códigos hash, como diccionarios o conjuntos.
        /// </summary>
        /// <returns>Un entero que representa el código hash de la falla.</returns>
        public override int GetHashCode()
            => HashCode.Combine(
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

        /// <summary>
        /// Devuelve una representación en cadena de la instancia actual de <see cref="Fault"/>, que incluye el código y el mensaje de la falla. Esta representación es útil para depuración y registro de errores.
        /// </summary>
        /// <returns>Una cadena que representa la falla.</returns>
        public override string ToString() => $"[{Code}] {Message}";

        /// <summary>
        ///  Valida si dos cadenas son iguales, ignorando mayúsculas y minúsculas. Esta función se utiliza para comparar el código y el mensaje de las fallas de manera que no se vean afectadas por diferencias en la capitalización.
        /// </summary>
        /// <param name="a">La primera cadena a comparar.</param>
        /// <param name="b">La segunda cadena a comparar.</param>
        /// <returns>true si las cadenas son iguales; de lo contrario, false.</returns>
        private static bool Validate(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}
