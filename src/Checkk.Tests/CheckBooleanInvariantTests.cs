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
                () => Check.That(() => test.Length == 8));
        }

        [Fact]
        public void FalseFails()
        {
            const string test = "Hi there";

            Should.Throw<InvariantShouldBeTrueException>(
                () => Check.That(() => test == string.Empty).IsTrue());
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const string test = "Hi there";

            Should
                .Throw<Exception>(() => Check.That(() => test.Length == 2).IsTrue())
                .Message.ShouldBe("(\"Hi there\".Length == 2) should be true");

            Should
                .Throw<Exception>(() => Check.That(() => test == string.Empty).IsTrue())
                .Message.ShouldBe("(\"Hi there\" == String.Empty) should be true");

            var test2 = "Why hello";
            Should
                .Throw<Exception>(() => Check.That(() => test2 == string.Empty).IsTrue())
                .Message.ShouldBe("(test2 == String.Empty) should be true");
        }

        [Fact]
        public void IsTrueCustomMessageIsUsed()
        {
            Should
                .Throw<InvariantShouldBeTrueException>(() => Check.That(() => false).IsTrue("custom message"))
                .Message.ShouldBe("custom message");
        }

        [Fact]
        public void TrueFailsIsFalse()
        {
            const string test = "Hi there";

            Should.Throw<InvariantShouldBeFalseException>(
                () => Check.That(() => test.Length == 8).IsFalse())
                .Message.ShouldBe("(\"Hi there\".Length == 8) should be false");
        }

        [Fact]
        public void FalsePassesIsFalse()
        {
            const string test = "Hi there";

            Should.NotThrow(
                () => Check.That(() => test == string.Empty).IsFalse());
        }

        [Fact]
        public void IsFalseCustomMessageIsUsed()
        {
            Should
                .Throw<InvariantShouldBeFalseException>(() => Check.That(() => true).IsFalse("custom message"))
                .Message.ShouldBe("custom message");
        }
    }
}