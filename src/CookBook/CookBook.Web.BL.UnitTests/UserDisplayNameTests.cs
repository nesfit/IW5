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
    public void PrefersUsernameToDisplayName()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("name", "Demo Teacher"), new Claim("preferred_username", "teacher@example.com")], "test"));

        Assert.Equal("teacher@example.com", user.GetDisplayName());
    }

    [Fact]
    public void DoesNotExposeOpaqueIdentifiersWhenReadableClaimsAreMissing()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim("sub", "opaque-subject"), new Claim("oid", "object-id"), new Claim("name", " ")], "test"));

        Assert.Equal("signed-in user", user.GetDisplayName());
    }
}
