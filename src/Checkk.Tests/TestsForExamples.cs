using System;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class TestsForExamples
    {
        [Fact]
        public void FirstSetOfReadmeExamples()
        {
            var firstName = "";
            var lastName = "Lastname";
            var dateOfBirth = new DateTime(1989, 1, 1);

            Check.Yourself(() => firstName).IsNotNull();
            Check.Yourself(() => lastName).IsNotNullOrEmpty();
            Check.Yourself(() => dateOfBirth.Year < 1990);

            var i1 = 10;
            var ex1 = Should.Throw<Exception>(() => Check.Yourself(() => i1 > 20));
            ex1.Message.ShouldBe("(i1 > 20) should be true");
        }
    }
}
