using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
    public interface IPrintingConfig
    {
        Dictionary<Type, Func<object, string>> AlternativeSerializersByType { get; }
        Dictionary<MemberInfo, Func<object, string>> AlternativeSerializersByName { get; }
    }

    public class PrintingConfig<TOwner> : IPrintingConfig
    {
        private List<Type> excludedTypes = new List<Type>();
        private List<MemberInfo> excludedFields = new List<MemberInfo>();
        private Dictionary<Type, Func<object, string>> alternativeSerializersByType =
            new Dictionary<Type, Func<object, string>>();
        private Dictionary<MemberInfo, Func<object, string>> alternativeSerializersByName =
            new Dictionary<MemberInfo, Func<object, string>>();

        Dictionary<Type, Func<object, string>> IPrintingConfig.AlternativeSerializersByType => alternativeSerializersByType;
        Dictionary<MemberInfo, Func<object, string>> IPrintingConfig.AlternativeSerializersByName => alternativeSerializersByName;

        public PrintingConfig<TOwner> ExcludeType<T>()
        {
            excludedTypes.Add(typeof(T));
            return this;
        }

        public SerializeConfig<TOwner, T> Printing<T>()
        {
            return new SerializeConfig<TOwner, T>(this);
        }

        public string PrintToString(TOwner obj)
        {
            return PrintToString(obj, 0);
        }

        public PrintingConfig<TOwner> Excluding<T>(Expression<Func<TOwner, T>> pr)
        {
            var member = pr.Body as MemberExpression;
            excludedFields.Add(member.Member);
            return this;
        }

        public PrintingConfig<TOwner> ByDefault()
        {
            return Printing<double>()
                .Using(new CultureInfo("en-US"))
            .Printing<DateTime>()
                .Using(date => String.Format("{0}/{1}/{2} {3}:{4}:{5}",
                    date.Month, date.Day, date.Year, date.Hour, date.Minute, date.Second))
            .Printing<TimeSpan>()
                .Using(time => String.Format("{0} {1}:{2}:{3}.{4}",
                    time.Days, time.Hours, time.Minutes, time.Seconds, time.Milliseconds))
            .Printing<string>()
                .Cut(20);
        }

        public PrintingConfig<TOwner> ByDefault<T>(Func<T, string> config)
        {
            return ByDefault().Printing<T>().Using(config);
        }

        public PropertySerializer<TOwner, T> Printing<T>(Expression<Func<TOwner, T>> pr)
        {
            var member = pr.Body as MemberExpression;
            return new PropertySerializer<TOwner, T>(this, member.Member);
        }

        public SerializeConfig<TOwner, string> Printing(Expression<Func<TOwner, string>> pr)
        {
            return new SerializeConfig<TOwner, string>(this, pr);
        }

        private string PrintToString(object obj, int nestingLevel)
        {
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
                if (!excludedTypes.Contains(propertyInfo.PropertyType) && !excludedFields.Contains(propertyInfo))
                {
                    var value = PrintToString(propertyInfo.GetValue(obj), nestingLevel + 1);
                    if (alternativeSerializersByType.ContainsKey(propertyInfo.PropertyType))
                        value = alternativeSerializersByType[propertyInfo.PropertyType](propertyInfo.GetValue(obj)) + Environment.NewLine;
                    if (alternativeSerializersByName.ContainsKey(propertyInfo))
                        value = alternativeSerializersByName[propertyInfo](propertyInfo.GetValue(obj)) + Environment.NewLine;
                    sb.Append(identation + propertyInfo.Name + " = " + value);
                }
            }
            return sb.ToString();
        }
    }
}