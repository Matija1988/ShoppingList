namespace Domain.Users;

public record struct UserId
{
    public Guid Value { get; set; }

    private UserId(Guid value) => Value = value;

    public static UserId Of(Guid value)
    {
        if(value == Guid.Empty)
        {
            throw new ArgumentException("UserId empty!");
        }
        return new UserId(value);
    }
}
