using System;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
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
                () => Check.Yourself(() => test).IsNotNull());
        }

        [Fact]
        public void OnNullFails()
        {
            Foo test = null;

            Should.Throw<InvariantShouldNotBeNullException>(
                () => Check.Yourself(() => test).IsNotNull());
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            Foo test = null;

            var exception = Should.Throw<Exception>(
                () => Check.Yourself(() => test).IsNotNull());

            exception.Message.ShouldBe("test should not be null");
        }
    }
}