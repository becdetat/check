using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckStringInvariantIsValidEmailAddress
    {
        [Fact]
        public void OnMatchPasses()
        {
            const string address = "address@domain.com";

            Should.NotThrow(() => Check.That(() => address).IsValidEmailAddress());
        }

        [Fact]
        public void OnNoMatchFails()
        {
            const string address = "address@domain";

            Should.Throw<InvariantShouldBeValidEmailAddressException>(
                () => Check.That(() => address).IsValidEmailAddress());
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const string address = "address@domain";

            var exception = Should.Throw<InvariantShouldBeValidEmailAddressException>(
                () => Check.That(() => address).IsValidEmailAddress());

            exception.Message.ShouldBe(
                @"""address@domain"" should be a valid email address");
        }
    }
}