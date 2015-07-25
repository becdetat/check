using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckIsNotEqualToTests
    {
        [Fact]
        public void PassesWhenValuesAreNotEqual()
        {
            var name = "Han";

            Should.NotThrow(() => Check.That(() => name).IsNotEqualTo("Jabba"));
        }

        [Fact]
        public void ThrowsWhenValuesAreEqual()
        {
            var name = "Chewbacca";

            Should.Throw<InvariantShouldNotBeEqualToException<string>>(() => Check.That(() => name).IsNotEqualTo("Chewbacca"));
        }

        [Fact]
        public void MessageIsCorrectWhenValuesAreEqual()
        {
            var name = "R2D2";

            Should.Throw<InvariantShouldNotBeEqualToException<string>>(() => Check.That(() => name).IsNotEqualTo("R2D2"))
                .Message.ShouldBe("\"R2D2\" should not be equal to \"R2D2\"");
        }
    }
}