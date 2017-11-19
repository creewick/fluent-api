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
		        .Excluding(p => p.Name);

            string s1 = printer.PrintToString(person);

			//7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию		
			//8. ...с конфигурированием
		}
	}
}