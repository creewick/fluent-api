using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner>
    {
        public PrintingConfig<TOwner> CutString(Expression<Func<string>> exp)
        {
            return this;
        }

        public PrintingConfig<TOwner> ExcludeType<T>()
        {
            return this;
        }

        public PrintingConfig<TOwner> SetupType<T>(Func<T, string> func)
        {
            return this;
        }

        public SerializeConfig<TOwner, T> Printing<T>()
        {
            return new SerializeConfig<TOwner, T>(this);
        }

        public PrintingConfig<TOwner> SetupCulture(CultureInfo culture)
        {
            return this;
        }

        public PrintingConfig<TOwner> SetupProperty(Func<PropertyInfo, string> func)
        {
            return this;
        }

        public string PrintToString(TOwner obj)
        {
            return PrintToString(obj, 0);
        }

        public PrintingConfig<TOwner> Excluding<T>(Func<TOwner, T> pr)
        {
            return this;
        }

        public PropertySerializer<TOwner, T> Printing<T>(Func<TOwner, T> pr)
        {
            return new PropertySerializer<TOwner, T>(this);
        }

        public StringSerializer<TOwner> Printing(Func<TOwner, string> pr)
        {
            return new StringSerializer<TOwner>(this);
        }

        private string PrintToString(object obj, int nestingLevel)
        {
            //TODO apply configurations
            if (obj == null)
                return "null" + Environment.NewLine;

            var finalTypes = new[]
            {
                typeof(int), typeof(double), typeof(float), typeof(string),
                typeof(DateTime), typeof(TimeSpan)
            };
            if (finalTypes.Contains(obj.GetType()))
                return obj + Environment.NewLine;

            var identation = new string('\t', nestingLevel + 1);
            var sb = new StringBuilder();
            var type = obj.GetType();
            sb.AppendLine(type.Name);
            foreach (var propertyInfo in type.GetProperties())
            {
                sb.Append(identation + propertyInfo.Name + " = " +
                          PrintToString(propertyInfo.GetValue(obj),
                              nestingLevel + 1));
            }
            return sb.ToString();
        }
    }

    public static class StringExtension
    {
        public static 
    }
}