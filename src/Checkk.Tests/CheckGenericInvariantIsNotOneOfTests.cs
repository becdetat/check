using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckGenericInvariantIsNotOneOfTests
    {
        [Fact]
        public void PassesWhenValueIsNotOne()
        {
            const int number = 7;

            Should.NotThrow(() => Check.That(() => number).IsNotOneOf(1, 2, 3, 4));
        }

        [Fact]
        public void FailsWhenValueIsOne()
        {
            const int number = 4;

            Should.Throw<InvariantShouldNotBeOneOfException<int>>(() => Check.That(() => number).IsNotOneOf(1, 2, 3, 4));
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const int number = 4;

            var exception = Should.Throw<InvariantShouldNotBeOneOfException<int>>(() => Check.That(() => number).IsNotOneOf(1, 2, 3, 4));

            exception.Message.ShouldBe(@"""4"" should not be one of [""1"",""2"",""3"",""4""]");
        }
    }
}