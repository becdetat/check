using System;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckBooleanInvariantTests
    {
        [Fact]
        public void TruePasses()
        {
            const string test = "Hi there";

            Should.NotThrow(
                () => Check.Yourself(() => test.Length == 8));
        }

        [Fact]
        public void FalseFails()
        {
            const string test = "Hi there";

            Should.Throw<InvariantShouldBeTrueException>(
                () => Check.Yourself(() => test == string.Empty));
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const string test = "Hi there";

            Should
                .Throw<Exception>(() => Check.Yourself(() => test.Length == 2))
                .Message.ShouldBe("(\"Hi there\".Length == 2) should be true");

            Should
                .Throw<Exception>(() => Check.Yourself(() => test == string.Empty))
                .Message.ShouldBe("(\"Hi there\" == String.Empty) should be true");

            var test2 = "Why hello";
            Should
                .Throw<Exception>(() => Check.Yourself(() => test2 == string.Empty))
                .Message.ShouldBe("(test2 == String.Empty) should be true");
        }
    }
}