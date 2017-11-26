using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    public class ObjectPrinterAcceptanceTests
    {
        [Test]
        public void Demo()
        {
            var person = new Person { Name = "Alex", Age = 19 };

            var printer = ObjectPrinter.For<Person>()
                //1. Исключить из сериализации свойства определенного типа
                .ExcludeType<int>()
                //2. Указать альтернативный способ сериализации для определенного типа
                .Printing<int>()
                .Using(p => p.ToString())
                //3. Для числовых типов указать культуру;
                .Printing<double>()
                .Using(CultureInfo.CurrentCulture)
                //4. Настроить сериализацию конкретного свойства
                .Printing(p => p.Age)
                .Using(p => p.ToString())
                //5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
                .Printing(p => p.Name)
                .Cut(10)
                //6. Исключить из сериализации конкретного свойства
                .Excluding(p => p.Name)
                //7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию		
                .ByDefault()
                //8. ...с конфигурированием
                .ByDefault<int>(n => 42.ToString());

            string s1 = printer.PrintToString(person);
        }

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
                .ExcludeType<int>();
            printer.PrintToString(person).IndexOf("Age").Should().Be(-1);
        }

        [Test]
        public void SetAlternativeSerializationForType()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing<int>()
                .Using(n => 42.ToString());
            printer.PrintToString(person).IndexOf("Age = 42").Should().NotBe(-1);
        }

        [Test]
        public void SetCulture()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing<double>()
                .Using(new CultureInfo("ru-RU"));
            printer.PrintToString(person).IndexOf("Height = 120,5").Should().NotBe(-1);
        }

        [Test]
        public void SetAlternativeSerializationForMember()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing(p => p.Age)
                .Using(n => 42.ToString());
            printer.PrintToString(person).IndexOf("Age = 42").Should().NotBe(-1);
        }

        [Test]
        public void CutString()
        {
            var printer = ObjectPrinter.For<Person>()
                .Printing(p => p.Name)
                .Cut(2);
            printer.PrintToString(person).IndexOf("Name = Al").Should().NotBe(-1);
        }

        [Test]
        public void ExcludingMember()
        {
            var printer = ObjectPrinter.For<Person>()
                .Excluding(p => p.Age);
            printer.PrintToString(person).IndexOf("Age").Should().Be(-1);
        }
	}
}