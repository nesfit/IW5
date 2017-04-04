using UserManagement.ConsoleApp.Emails;
using Xunit;

namespace UserManagement.Tests.Emails
{
    public class NewsletterFactoryTests
    {
        NewsletterFactory newsletterFactorySUT = new NewsletterFactory();

        [Fact]
        public void CreateNewsletter_Subject_Correct()
        {
            var newsletter = newsletterFactorySUT.CreateNewsletter();

            var expectedMessage = "Máme pro vás jedinečnou nabídku!";
            Assert.Equal(expectedMessage, newsletter.Subject);
        }

        [Fact]
        public void CreateNewsletter_Body_Correct()
        {
            var newsletter = newsletterFactorySUT.CreateNewsletter();

            var expectedMessage = "Začněte používat návrhové vzory právě teď!";
            Assert.Equal(expectedMessage, newsletter.Body);
        }
    }
}