using Microsoft.AspNetCore.Http;
using Resolute.Common.Problems;
using Resolute.Failures;
using Resolute.Results;

namespace Resolute.Tests;

public class ResultHttpExtensionsTests
{
    [Fact]
    public void ToOkResult_OnSuccess_ShouldReturn200WithPayload()
    {
        Result<int> result = 42;

        IResult httpResult = result.ToOkResult();
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(httpResult);
        IValueHttpResult valueResult = Assert.IsAssignableFrom<IValueHttpResult>(httpResult);

        Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);
        Assert.Equal(42, Assert.IsType<int>(valueResult.Value));
    }

    [Fact]
    public void ToCreatedResult_OnSuccess_ShouldReturn201AndLocation()
    {
        Result<string> result = "abc";

        IResult httpResult = result.ToCreatedResult(value => $"/items/{value}");
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(httpResult);
        IValueHttpResult valueResult = Assert.IsAssignableFrom<IValueHttpResult>(httpResult);
        string location = Assert.IsType<string>(httpResult.GetType().GetProperty("Location")?.GetValue(httpResult));

        Assert.Equal(StatusCodes.Status201Created, statusCodeResult.StatusCode);
        Assert.Equal("abc", Assert.IsType<string>(valueResult.Value));
        Assert.Equal("/items/abc", location);
    }

    [Fact]
    public void ToNoContentResult_OnSuccess_ShouldReturn204()
    {
        Result<int> result = 10;

        IResult httpResult = result.ToNoContentResult();
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(httpResult);

        Assert.Equal(StatusCodes.Status204NoContent, statusCodeResult.StatusCode);
    }

    [Fact]
    public void ToIResult_OnAggregateFault_ShouldReturnValidationProblem()
    {
        AggregateFault aggregateFault = AggregateFault.From([
            Faults.Required("Name"),
            Faults.Required("Email")
        ]);

        Result<int> result = Result<int>.Failure(aggregateFault);

        IResult httpResult = result.ToOkResult();
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(httpResult);
        IValueHttpResult valueResult = Assert.IsAssignableFrom<IValueHttpResult>(httpResult);
        object problem = Assert.IsType<HttpValidationProblemDetails>(valueResult.Value);

        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        Assert.IsType<HttpValidationProblemDetails>(problem);
    }

    [Fact]
    public void ToIResult_OnHttpFault_ShouldRespectHttpStatusCode()
    {
        HttpFault fault = FaultsHttp.NotImplemented("Feature.Pending");
        Result<int> result = Result<int>.Failure(fault);

        IResult httpResult = result.ToOkResult();
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(httpResult);

        Assert.Equal(StatusCodes.Status501NotImplemented, statusCodeResult.StatusCode);
    }

}
