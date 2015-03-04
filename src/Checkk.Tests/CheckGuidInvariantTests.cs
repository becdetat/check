using System;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckGuidInvariantTests
    {
        [Fact]
        public void NotEmptyPassesIsNotNullOrEmpty()
        {
            var guid = Guid.NewGuid();

            Should.NotThrow(() => Check.Yourself(() => guid).IsNotNullOrEmpty());
        }

        [Fact]
        public void EmptyFailsIsNotNullOrEmpty()
        {
            var guid = Guid.Empty;

            Should.Throw<InvariantShouldNotBeNullOrEmptyException>(
                () => Check.Yourself(() => guid).IsNotNullOrEmpty());
        }

        [Fact]
        public void NullFailsIsNotNullOrEmpty()
        {
            var guid = default(Guid);

            Should.Throw<InvariantShouldNotBeNullOrEmptyException>(
                () => Check.Yourself(() => guid).IsNotNullOrEmpty());
        }
    }
}
