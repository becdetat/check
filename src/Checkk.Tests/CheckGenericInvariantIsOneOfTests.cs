using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckGenericInvariantIsOneOfTests
    {
        [Fact]
        public void PassesWhenValueIsOne()
        {
            const int number = 4;

            Should.NotThrow(() => Check.That(() => number).IsOneOf(1, 2, 3, 4));
        }

        [Fact]
        public void FailsWhenValueIsNotOne()
        {
            const int number = 7;

            Should.Throw<InvariantShouldBeOneOfException<int>>(() => Check.That(() => number).IsOneOf(1, 2, 3, 4));
        }

        [Fact]
        public void FailureMessageIsValid()
        {
            const int number = 7;

            var exception = Should.Throw<InvariantShouldBeOneOfException<int>>(() => Check.That(() => number).IsOneOf(1, 2, 3, 4));

            exception.Message.ShouldBe(@"""7"" should be one of [""1"",""2"",""3"",""4""]");
        }
    }
}
