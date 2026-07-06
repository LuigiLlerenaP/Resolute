using Resolute.Common.Problems;

namespace Resolute.Failures
{
    /// <summary>
    /// Representa un fallo que contiene múltiples fallos individuales.
    /// </summary>
    /// <param name="faults">La lista de fallos individuales que componen este fallo aggregate.</param>
    public sealed class AggregateFault(IReadOnlyList<Fault> faults) : Fault(
            code: "Fault.Aggregate",
            message: BuildSummary(faults))

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
        /// Devuelve una representación legible del <see cref="AggregateFault"/>.
        /// Incluye el código agregado, la cantidad de fallos y los códigos individuales.
        /// </summary>
        /// <returns>
        /// Formato: <c>[Fault.Aggregate] 2 fault(s) → E1, E2</c>
        /// </returns>
        public override string ToString()
            => $"[{Code}] {Faults.Count} fault(s) → {string.Join(", ", Faults.Select(f => f.Code))}";

        /// <summary>
        /// Construye el resumen de códigos que se embebe en el <see cref="Fault.Message"/> base.
        /// Se evalúa en el constructor antes de que <see cref="Faults"/> esté disponible,
        /// por eso recibe la lista como parámetro en lugar de usar la propiedad.
        /// </summary>
        /// <param name="faults">Lista de fallos a resumir.</param>
        /// <returns>
        /// Formato: <c>E1, E2, E3</c>
        /// </returns>
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
