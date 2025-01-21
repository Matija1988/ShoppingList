namespace SharedCommon;
public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorTypes.Failure);
    public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorTypes.Failure);

    public Error(string code, string description, ErrorTypes type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; set; }
    public string Description { get; set; }
    public ErrorTypes Type { get; set; }

    public static Error Failure(string code, string description) => new(code, description, ErrorTypes.Failure);
    public static Error NotFound(string code, string description) => new(code, description, ErrorTypes.NotFound);
    public static Error Problem(string code, string description) => new(code, description, ErrorTypes.Problem);
    public static Error Conflict(string code, string description) => new(code, description, ErrorTypes.Conflict);
}
