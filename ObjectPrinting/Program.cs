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
            var person = new Person { Name = "Alex", Age = 19, A = new A { B = 0.2 } };

            var printer = ObjectPrinter.For<Person>()
                .ByDefault()
                .Printing<A>()
                .Using(p => p.ToString())
                .Printing<double>()
                .Using(CultureInfo.CurrentCulture)
                .Build();
            string s1 = printer.PrintToString(person);
            
            
            Console.WriteLine(s1);
            Console.ReadLine();
        }
    }
}
