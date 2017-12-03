using System.Globalization;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
    [TestFixture]
    public class ObjectPrinterAcceptanceTests
    {
        [Test]
        public void Demo()
        {
            var person = new Person { Name = "Alex", Age = 19, A = new A { B = 0.2 } };
            
            //1. Исключить из сериализации свойства определенного типа
            string s1 = ObjectPrinter.For<Person>()
                        .ExcludeType<int>()
                        .Build()
                        .PrintToString(person);
            //2. Указать альтернативный способ сериализации для определенного типа
            string s2 = ObjectPrinter.For<Person>()
                        .Printing<int>()
                        .Using(p => p.ToString())
                        .Build()
                        .PrintToString(person);
            //3. Для числовых типов указать культуру;
            string s3 = ObjectPrinter.For<Person>()
                        .Printing<double>()
                        .Using(CultureInfo.CurrentCulture)
                        .Build()
                        .PrintToString(person);
            //4. Настроить сериализацию конкретного свойства
            string s4 = ObjectPrinter.For<Person>()
                        .Printing(p => p.Age)
                        .Using(p => p.ToString())
                        .Build()
                        .PrintToString(person);
            //5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
            string s5 = ObjectPrinter.For<Person>()
                        .Printing(p => p.Name)
                        .Cut(10)
                        .Build()
                        .PrintToString(person);
            //6. Исключить из сериализации конкретного свойства
            string s6 = ObjectPrinter.For<Person>()
                        .Excluding(p => p.Name)
                        .Build()
                        .PrintToString(person);
            //7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию
            string s7 = ObjectPrinter.For<Person>()
                        .ByDefault()
                        .PrintToString(person);
            //8. ...с конфигурированием
            string s8 = ObjectPrinter.For<Person>()
                        .ByDefault(config => config
                            .Printing<string>()
                                .Cut(20)
                            .Printing<int>()
                                .Using(n => (2*n).ToString()))
                        .PrintToString(person);
        }
	}
}