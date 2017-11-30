using System;

namespace ObjectPrinting.Tests
{
	public class Person
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public double Height { get; set; }
		public int Age { get; set; }
        public A A { get; set; }
	}

    public class A
    {
        public double B { get; set; }
    }
}