namespace DependencyInjection.Web;

public class UserDatabase
{
    public readonly List<UserEntity> Users = [];
    
    public static readonly UserDatabase Instance = new();
}

public record UserEntity(
    Guid Id,
    string Email,
    string Password);