namespace SharedCommon;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
