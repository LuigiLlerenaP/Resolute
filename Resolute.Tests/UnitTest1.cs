using Resolute.Common.Problems;
using Resolute.Failures;
using Resolute.Results;

namespace Resolute.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Combine_SomeFailures_ShouldNotStopAtFirstFault()
        {
            List<int> trackedValues = [];
            List<int> items = [-1, -2, 3];

            IEnumerable<Result<int>> results = items
                .Select(v => ValidateAndTrack(v, ref trackedValues));

            Result<IReadOnlyList<int>> combined = results.Combine();

            Assert.Equal(3, trackedValues.Count);

            AggregateFault aggregate = Assert.IsType<AggregateFault>(combined.Fault);

            Assert.Null(combined.Value);

            Assert.Equal(2, aggregate.Faults.Count);
        }

        [Fact]
        public void Bind_OnSuccess_ShouldExecuteNextOperation()
        {
            Result<int> result = 5;

            Result<string> newResult = result.Bind(FormatAsPositive); //Devuleve elr error anterior si hay o el numo o suvalor actual 

            Assert.True(newResult.IsSuccess);

            Assert.Equal("positivo:5", newResult.Value);
        }

        [Fact]
        public void Bind_OnFailure_ShouldShortCircuit()
        {
            Result<int> result = -5;

            Result<string> newResult = result.Bind(FormatAsPositive);

            Assert.True(newResult.IsFailure);
            Assert.Null(newResult.Value);
        }

        [Fact]
        public void From_WithSimpleFaults_ShouldGroupThem()
        {
            const string password = "inputNumber";
            Fault fault1 = Faults.Invalid(nameof(password), password);
            Fault fault2 = Faults.Required("inputNumber");

            AggregateFault aggregateFault = AggregateFault.From([fault1, fault2]);

            Assert.Equal(fault1, aggregateFault.Faults[0]);
            Assert.Equal(fault2, aggregateFault.Faults[1]);
        }

        [Fact]
        public void ToString_ShouldListAllCodes()
        {
            const string password = "E1";
            Fault fault1 = Faults.Invalid(nameof(password), password);
            Fault fault2 = Faults.Required("E2");

            AggregateFault aggregateFault = AggregateFault.From([fault1, fault2]);

            string result = aggregateFault.ToString();

            Assert.Contains("E1", result);
            Assert.Contains("E2", result);
        }

        [Fact]
        public void Match_OnFailure_ShouldExecuteFailureBranch()
        {
            int number = -5;
            Result<int> result = Faults.Invalid(nameof(number), number);

            string outResultExcec = result.Match(   //DEVULVE EL RESULTADO DEL RESULT DE LA OPERACION  EN BASE AL RESULT
                onSuccess: (v) => $"Value:{v}" ,
                onFailure: (err) => $"Error: {err}" 
            );

            Assert.Contains(nameof(number), outResultExcec);

        }

        [Fact]
        public void Map_OnSuccess_ShouldTransformValue()
        {
            // Arrange
            Result<int> successResult = 5;

            // Act
            Result<string> mappedResult = successResult.Map(FormatNumber);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("N[5]", mappedResult.Value);
        }

        [Fact]
        public void Ensure_PredicatePasses_ShouldKeepSuccess()
        {
            Result<int> result = 10;
            Result<int> outcome = result.Ensure(ValidateNumber, Faults.Invalid("Cantidad", result.Value));

            Assert.True(outcome.IsSuccess);
            Assert.Equal(10, outcome.Value);
        }


        private static Result<int> ValidateAndTrack(int inputNumber, ref List<int> trackedValues)
        {
            trackedValues.Add(inputNumber);

            if (inputNumber < 0)
            {
                return Faults.Invalid(nameof(inputNumber) , inputNumber);
            }

            return inputNumber;
        }

        private static Result<string> FormatAsPositive(int inputNumber)
        {
            if (inputNumber < 0)
            {
                return Faults.Invalid(nameof(inputNumber) , inputNumber);
            }

            return $"positivo:{inputNumber}";
        }

        private static string FormatNumber(int number) => $"N[{number}]";

        private static bool ValidateNumber( int number) => number >= 5;
    }
}
