using Check.Exceptions;
using Shouldly;
using Xunit;

namespace Check.Tests
{
    public class CheckTypeInvariantHasConstructorWithParameters
    {
        class Foo 
        {
            public Foo(string s, int i)
            {
            }
        }

        [Fact]
        public void MatchingParameterTypesPass()
        {
            Should.NotThrow(() =>
                Check.That(() => typeof (Foo)).HasPublicConstructorWithParameters(
                    typeof (string),
                    typeof (int)));
        }

        [Fact]
        public void NonMatchingParameterTypesFail()
        {
            Should.Throw<InvariantShouldHavePublicConstructorWithParametersException>(
                () => Check.That(() => typeof(Foo)).HasPublicConstructorWithParameters(
                    typeof(float)));
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            var exception =
                Should.Throw<InvariantShouldHavePublicConstructorWithParametersException>(
                    () => Check.That(() => typeof (Foo)).HasPublicConstructorWithParameters(
                        typeof (float)));

            exception.Message.ShouldBe(
                "Check.Tests.CheckTypeInvariantHasConstructorWithParameters+Foo should have a public constructor accepting (Single)");
        }
    }
}