using ObjectPrinting.Tests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    class Program
    {
        static void Main()
        {
            var person = new Person { Name = "Alex", Age = 19, Height = 20.5 };

            var printer = ObjectPrinter.For<Person>()
                .Excluding(p => p.Name);
            string s1 = printer.PrintToString(person);

            Console.WriteLine(s1);
            Console.ReadLine();
        }
    }
}
