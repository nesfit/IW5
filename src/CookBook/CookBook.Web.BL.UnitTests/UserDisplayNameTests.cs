using System.Security.Claims;
using CookBook.Web.BL.Extensions;
using Xunit;

namespace CookBook.Web.BL.UnitTests;

public class UserDisplayNameTests
{
    [Theory]
    [InlineData("username", "cookbook-user")]
    [InlineData("preferred_username", "teacher@example.com")]
    [InlineData("name", "Demo Teacher")]
    [InlineData(ClaimTypes.Name, "Demo Teacher")]
    [InlineData("email", "teacher@example.com")]
    public void UsesReadableClaimInsteadOfSubject(string claimType, string value)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("sub", "opaque-subject"), new Claim(claimType, value)], "test"));

        Assert.Equal(value, user.GetDisplayName());
    }

    [Fact]
    public void PrefersPersonNameToUsername()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("username", "cookbook-user"),
                new Claim("preferred_username", "teacher@example.com"),
                new Claim("name", "Demo Teacher")
            ], "test"));

        Assert.Equal("Demo Teacher", user.GetDisplayName());
    }

    [Fact]
    public void CombinesGivenAndFamilyNamesWhenFullNameIsMissing()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("preferred_username", "teacher@example.com"),
                new Claim("given_name", "Demo"),
                new Claim("family_name", "Teacher")
            ], "test"));

        Assert.Equal("Demo Teacher", user.GetDisplayName());
    }

    [Fact]
    public void DoesNotExposeOpaqueIdentifiersWhenReadableClaimsAreMissing()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("sub", "opaque-subject"), new Claim("oid", "object-id"), new Claim("name", " ")], "test"));

        Assert.Equal("signed-in user", user.GetDisplayName());
    }
}
