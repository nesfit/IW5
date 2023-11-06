namespace CookBook.IdentityProvider.BL.Models;

public class AppUserCreateModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Subject { get; set; }
    public required string Email { get; set; }
    public required string GivenName { get; set; }
    public required string FamilyName { get; set; }
}
