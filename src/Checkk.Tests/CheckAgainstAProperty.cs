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
}