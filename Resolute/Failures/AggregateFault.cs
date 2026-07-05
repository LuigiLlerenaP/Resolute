namespace Resolute.Faults
{
    /// <summary>
    /// Representa un fallo que contiene múltiples fallos individuales.
    /// </summary>
    /// <param name="faults">La lista de fallos individuales que componen este fallo aggregate.</param>
    public sealed class AggregateFault(IReadOnlyList<Fault> faults) : Fault(
            code: "Fault.Aggregate",
            message: $"{faults.Count} fault(s): {BuildSummary(faults)}")

    {
        /// <summary>
        ///  Obtiene la lista de fallos individuales que componen este fallo aggregate.
        /// </summary>
        public IReadOnlyList<Fault> Faults { get; } = faults;

        /// <summary>
        ///  Crea un <see cref="AggregateFault"/> a partir de una colección de <see cref="Fault"/>.
        /// </summary>
        /// <param name="faults">La lista de fallos individuales que componen este fallo aggregate.</param>
        /// <returns> Retorna un nuevo <see cref="AggregateFault"/> con los fallos proporcionados. </returns>
        public static AggregateFault From(IEnumerable<Fault> faults)
            => new(faults.SelectMany(Flatten).ToList().AsReadOnly());

        /// <summary>
        /// Devuelve una representación en cadena del <see cref="AggregateFault"/>, incluyendo su código y los códigos de los fallos individuales.
        /// </summary>
        /// <returns>Un string que representa el fallo aggregate.</returns>
        public override string ToString()
            => $"[{Code}] {string.Join(", ", Faults.Select(f => f.Code))}";

        /// <summary>
        /// Construye un resumen de los códigos de los fallos individuales en una cadena separada por comas.
        /// </summary>
        /// <param name="faults">La lista de fallos individuales que componen este fallo aggregate.</param>
        /// <returns>Un string que resume los códigos de los fallos individuales.</returns>
        private static string BuildSummary(IReadOnlyList<Fault> faults)
           => string.Join(", ", faults.Select(f => f.Code));
       
        /// <summary>
        /// Aplana un <see cref="Fault"/> en sus componentes individuales.
        /// Si el fault es un <see cref="AggregateFault"/>, devuelve su lista interna de faults.
        /// Si es un <see cref="Fault"/> simple, lo devuelve envuelto en una colección de un elemento.
        /// Se usa en <see cref="From"/> para evitar <see cref="AggregateFault"/> anidados.
        /// </summary>
        /// <param name="fault">Fault a aplanar.</param>
        /// <returns>
        /// Los faults individuales que componen <paramref name="fault"/>,
        /// o una colección de un solo elemento si ya era un <see cref="Fault"/> simple.
        /// </returns>
        private static IEnumerable<Fault> Flatten(Fault fault)
        {
            if (fault is AggregateFault af)
            {
                return af.Faults;
            }

            return [fault];
        }
    }
}
