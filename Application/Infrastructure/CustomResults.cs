using SharedCommon;

namespace Web.Api.Infrastructure;
public static class CustomResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetError(result)
        );
    }

    private static string? GetTitle(Error error) => error.Type switch
    {
        ErrorTypes.Validation => error.Code,
        ErrorTypes.Problem => error.Code,
        ErrorTypes.NotFound => error.Code,
        ErrorTypes.Conflict => error.Code,
        _ => "Server failure!"
    };

    private static string GetDetail(Error error) => error.Type switch
    {
        ErrorTypes.Validation => error.Description,
        ErrorTypes.Problem => error.Description,
        ErrorTypes.NotFound => error.Description,
        ErrorTypes.Conflict => error.Description,
        _ => "An unexpected error occured!"
    };

    private static string GetType(ErrorTypes type) => type switch
    {
        ErrorTypes.Validation => ErrorDescriptions.Describe400(),
        ErrorTypes.Problem => ErrorDescriptions.Describe400(),
        ErrorTypes.NotFound => ErrorDescriptions.Describe404(),
        ErrorTypes.Conflict => ErrorDescriptions.Describe409(),
        _ => ErrorDescriptions.Describe500(),
    };

    private static int GetStatusCode(ErrorTypes type) => type switch
    {
        ErrorTypes.Validation => StatusCodes.Status400BadRequest,
        ErrorTypes.NotFound => StatusCodes.Status404NotFound,
        ErrorTypes.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };

    private static IDictionary<string, object?>? GetError(Result result)
    {
        if(result.Error is not ValidationError validationError)
        {
            return null;
        }

        return new Dictionary<string, object?>
        {
            {"Errors", validationError.Errors }
        };
    }
}
