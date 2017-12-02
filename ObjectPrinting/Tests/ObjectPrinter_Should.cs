using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    public class ObjectPrinter_Should
    {
        private Person person;

        [SetUp]
        public void SetUp()
        {
            person = new Person { Name = "Alex", Age = 19, Height = 120.5 };
        }

        [Test]
        public void ExcludeType()
        {
            var printer = ObjectPrinter.For<Person>()
                .ExcludeType<int>()
                .Build();
            printer.PrintToString(person).Should().NotContain("Age");
        }

        [Test]
        public void SetAlternativeSerializationForType()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing<int>()
                .Using(n => 42.ToString())
                .Build();
            printer.PrintToString(person).Should().Contain("Age = 42");
        }

        [Test]
        public void SetCulture()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing<double>()
                .Using(new CultureInfo("ru-RU"))
                .Build();
            printer.PrintToString(person).Should().Contain("Height = 120,5");
        }

        [Test]
        public void SetAlternativeSerializationForMember()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing(p => p.Age)
                .Using(n => 42.ToString())
                .Build();
            printer.PrintToString(person).Should().Contain("Age = 42");
        }

        [Test]
        public void CutString()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing(p => p.Name)
                .Cut(2)
                .Build();
            printer.PrintToString(person).Should().Contain("Name = Al\r\n");
        }

        [Test]
        public void ExcludingMember()
        {
            var printer = ObjectPrinter.For<Person>()
                .Excluding(p => p.Age)
                .Build();
            printer.PrintToString(person).Should().NotContain("Age");
        }
    }
}
