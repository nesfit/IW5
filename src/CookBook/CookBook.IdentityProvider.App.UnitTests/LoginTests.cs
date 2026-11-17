using System.Security.Claims;
using CookBook.IdentityProvider.App.Options;
using CookBook.IdentityProvider.App.Pages.Login;
using CookBook.IdentityProvider.BL.Facades;
using CookBook.IdentityProvider.BL.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CookBook.IdentityProvider.App.UnitTests;

public class LoginTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("/account", false)]
    [InlineData("https://example.com/", true)]
    public async Task ValidLoginAcceptsOptionalReturnUrlAndRejectsExternalRedirects(string? returnUrl, bool rejected)
    {
        var users = new Mock<IAppUserFacade>();
        var user = new AppUserDetailModel { Id = Guid.NewGuid(), UserName = "local-admin", Subject = "subject" };
        users.Setup(facade => facade.ValidateCredentialsAsync("local-admin", "password")).ReturnsAsync(true);
        users.Setup(facade => facade.GetUserByUserNameAsync("local-admin")).ReturnsAsync(user);
        var claims = new Mock<IAppUserClaimsFacade>();
        claims.Setup(facade => facade.GetAppUserClaimsByUserIdAsync(user.Id))
            .ReturnsAsync(Array.Empty<AppUserClaimListModel>());
        var interaction = new Mock<IIdentityServerInteractionService>();
        var authentication = new Mock<IAuthenticationService>();
        authentication.Setup(service => service.SignInAsync(
                It.IsAny<HttpContext>(), It.IsAny<string?>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties?>()))
            .Returns(Task.CompletedTask);
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMvc();
        services.AddSingleton(new IdentityServerOptions { Authentication = { CookieAuthenticationScheme = "test" } });
        services.AddSingleton(authentication.Object);
        using var provider = services.BuildServiceProvider();
        var httpContext = new DefaultHttpContext { RequestServices = provider };
        var page = new CookBook.IdentityProvider.App.Pages.Login.Index(interaction.Object, Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IIdentityProviderStore>(), Mock.Of<IEventService>(), users.Object, claims.Object,
            Microsoft.Extensions.Options.Options.Create(new CookBookIdentityProviderOptions()))
        {
            PageContext = new PageContext(new ActionContext(httpContext, new RouteData(), new ActionDescriptor())),
            Input = new InputModel { Username = "local-admin", Password = "password", Button = "login", ReturnUrl = returnUrl }
        };
        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.IsLocalUrl("/account")).Returns(true);
        page.Url = urlHelper.Object;
        provider.GetRequiredService<IObjectModelValidator>().Validate(page.PageContext, null, "Input", page.Input);

        Assert.True(page.ModelState.IsValid);
        if (rejected)
        {
            var error = await Assert.ThrowsAsync<Exception>(() => page.OnPost());
            Assert.Equal("invalid return URL", error.Message);
        }
        else
        {
            var redirect = Assert.IsType<RedirectResult>(await page.OnPost());
            Assert.Equal(returnUrl ?? "~/", redirect.Url);
        }
        authentication.Verify(service => service.SignInAsync(
            httpContext, It.IsAny<string?>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties?>()), Times.Once);
    }

    [Fact]
    public void MissingCredentialsAreStillInvalid()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMvc();
        using var provider = services.BuildServiceProvider();
        var context = new ActionContext(new DefaultHttpContext { RequestServices = provider },
            new RouteData(), new ActionDescriptor());

        provider.GetRequiredService<IObjectModelValidator>().Validate(
            context, null, "Input", new InputModel { Button = "login" });

        Assert.False(context.ModelState.IsValid);
        Assert.True(context.ModelState.ContainsKey("Input.Username"));
        Assert.True(context.ModelState.ContainsKey("Input.Password"));
        Assert.False(context.ModelState.ContainsKey("Input.ReturnUrl"));
    }
}
