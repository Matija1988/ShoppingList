namespace App.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
