using System;
using Check.Exceptions;
using Shouldly;
using Xunit;

namespace Check.Tests
{
    public class CheckGenericInvariantIsNotNullTests
    {
        private class Foo
        {
        }

        [Fact]
        public void OnNonNullPasses()
        {
            var test = new Foo();

            Should.NotThrow(
                () => Check.That(() => test).IsNotNull());
        }

        [Fact]
        public void OnNullFails()
        {
            Foo test = null;

            Should.Throw<InvariantShouldNotBeNullException>(
                () => Check.That(() => test).IsNotNull());
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            Foo test = null;

            var exception = Should.Throw<Exception>(
                () => Check.That(() => test).IsNotNull());

            exception.Message.ShouldBe("test should not be null");
        }
    }
}