using Checkk.Exceptions;
using Shouldly;
using Xunit;

namespace Checkk.Tests
{
    public class CheckAgainstAProperty
    {
        [Fact]
        public void CheckAgainstSetPropertyWorks()
        {
            var person = new Person
            {
                Name = "Chancellor Palpatine"
            };

            Should.NotThrow(() => Check.That(() => person.Name).IsNotNullOrEmpty());
        }

        [Fact]
        public void CheckingAgainstUnsetPropertyWorks()
        {
            var person = new Person();

            Should.Throw<InvariantShouldNotBeNullOrEmptyException>(() => Check.That(() => person.Name).IsNotNullOrEmpty());
        }

        private class Person
        {
            public string Name { get; set; }
        }
    }

    public class CheckAgainstConstant
    {
        [Fact]
        public void CheckAgainstConstantWorks()
        {
            const string name = "Lando";

            Should.Throw<InvariantShouldBeEqualToException<string>>(() => Check.That(() => name).IsEqualTo("Ackbar"));
        }

        [Fact]
        public void CheckAgainstConstantHasNiceMessage()
        {
            const string name = "Biggs";

            Should.Throw<InvariantShouldBeEqualToException<string>>(() => Check.That(() => name).IsEqualTo("Dooku"))
                .Message.ShouldBe("\"Biggs\" should be equal to \"Dooku\"");
        }
    }
}