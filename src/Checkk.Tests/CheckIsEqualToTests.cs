using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckIsEqualToTests
    {
        [Fact]
        public void PassesWhenValuesAreEqual()
        {
            const string name = "Palpatine";

            Should.NotThrow(() => Check.That(() => name).IsEqualTo("Palpatine"));
        }

        [Fact]
        public void ThrowsWhenValuesAreNotEqual()
        {
            const string name = "Yoda";

            Should.Throw<InvariantShouldBeEqualToException<string>>(() => Check.That(() => name).IsEqualTo("Luke"));
        }

        [Fact]
        public void MessageIsCorrectWhenValuesAreNotEqual()
        {
            const string name = "Obi-wan";

            Should.Throw<InvariantShouldBeEqualToException<string>>(() => Check.That(() => name).IsEqualTo("Leia"))
                .Message.ShouldBe("\"Obi-wan\" should be equal to \"Leia\"");
        }
    }
}