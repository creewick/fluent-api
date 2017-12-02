using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ObjectPrinting
{
    public class PrintingConfig<TOwner> : IPrintingConfig
    {
        private HashSet<Type> excludedTypes = new HashSet<Type>();
        private HashSet<MemberInfo> excludedFields = new HashSet<MemberInfo>();
        private Dictionary<Type, Func<object, string>> alternativeSerializersByType =
            new Dictionary<Type, Func<object, string>>();
        private Dictionary<MemberInfo, Func<object, string>> alternativeSerializersByName =
            new Dictionary<MemberInfo, Func<object, string>>();
        private HashSet<Type> finalTypes = new HashSet<Type>()
        {
                typeof(string), typeof(decimal), typeof(Guid),
                typeof(DateTime), typeof(TimeSpan)
        };

        Dictionary<Type, Func<object, string>> IPrintingConfig.AlternativeSerializersByType => alternativeSerializersByType;
        Dictionary<MemberInfo, Func<object, string>> IPrintingConfig.AlternativeSerializersByName => alternativeSerializersByName;
        HashSet<Type> IPrintingConfig.FinalTypes => finalTypes;
        HashSet<Type> IPrintingConfig.ExcludedTypes => excludedTypes;
        HashSet<MemberInfo> IPrintingConfig.ExcludedFields => excludedFields;

        public PrintingConfig<TOwner> ExcludeType<T>()
        {
            excludedTypes.Add(typeof(T));
            return this;
        }

        public TypePrintingConfig<TOwner, T> Printing<T>()
        {
            return new TypePrintingConfig<TOwner, T>(this);
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

        public PropertyPriningConfig<TOwner, T> Printing<T>(Expression<Func<TOwner, T>> pr)
        {
            var member = pr.Body as MemberExpression;
            return new PropertyPriningConfig<TOwner, T>(this, member.Member);
        }

        public ObjectPrinter Build()
        {
            return new ObjectPrinter(this);
        }
    }
}