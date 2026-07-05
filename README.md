# Resolute


## Overview

Resolute provides a lightweight result type (Result<T>) and a set of helpers to express success/failure flows without using exceptions for normal control flow. It includes:

- Result<T> representing either a successful value or a Fault
- Fault and AggregateFault to model errors
- Extension methods: Ensure, Map, Bind and Combine to compose operations

This library is designed for modern .NET (targeting .NET 10) and encourages explicit error handling and easy composition of operations that can fail.

## Features

- Strongly-typed success/failure results
- Implicit conversions to simplify returning values or faults
- Composable extension methods for fluent pipelines
- Combine to aggregate multiple results and collect faults

## Installation

Add the project to your solution or reference it as a library. If published as a NuGet package, install via:

```powershell
dotnet add package Resolute
```

Or add the project reference in your solution:

```xml
<ProjectReference Include="..\Resolute\Resolute.csproj" />
```

## Quick start

Create a successful result:

```csharp
var success = Result<int>.Success(42);
// or implicitly
Result<int> r = 42;
```

Create a failure:

```csharp
var fault = new Fault("NotFound", "Item not found");
var failure = Result<int>.Failure(fault);
// or implicitly
Result<int> r = fault;
```

Use Match to handle both cases:

```csharp
var message = r.Match(
	onSuccess: v => $"Value: {v}",
	onFailure: f => $"Error: {f.Message}"
);
```

Compose operations with Ensure, Map and Bind:

```csharp
var checkedResult = success.Ensure(v => v > 0, new Fault("Invalid", "Must be positive"));
var mapped = success.Map(v => v.ToString());
var bound = success.Bind(v => Result<string>.Success((v * 2).ToString()));
```

Combine multiple results and collect faults:

```csharp
var results = new[] { Result<int>.Success(1), Result<int>.Failure(new Fault("X","x")) };
var combined = results.Combine();
if (combined.IsFailure) { /* combined.Fault may be an AggregateFault */ }
```

## Design notes

- Result<T> throws when creating a success with a null value or a failure with a null fault to avoid invalid states.
- Extension methods are pure helpers that do not throw for normal failure cases; they propagate or aggregate faults.

## Contributing

Contributions are welcome. Please add unit tests for new behavior and follow the project's coding and documentation conventions.

## License

Include your license here (e.g. MIT) or place the appropriate license file at the repository root.
