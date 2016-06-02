using System.Text.RegularExpressions;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckStringInvariantIsMatchForRegexTest
    {
        private readonly Regex _emailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        [Fact]
        public void OnMatchPasses()
        {
            const string address = "address@domain.com";

            Should.NotThrow(
                () => Check.That(() => address).IsMatchForRegex(_emailRegex));
        }

        [Fact]
        public void OnNoMatchFails()
        {
            const string address = "address@domain";

            Should.Throw<InvariantShouldMatchRegexException>(
                () => Check.That(() => address).IsMatchForRegex(_emailRegex));
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const string address = "address@domain";

            var exception = Should.Throw<InvariantShouldMatchRegexException>(
                () => Check.That(() => address).IsMatchForRegex(_emailRegex));

            exception.Message.ShouldBe(
                @"""address@domain"" should match regular expression ""^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$""");
        }
    }
}