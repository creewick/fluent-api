using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner>, IPrintingConfig, ISerializeConfig<TOwner, TType>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        private readonly Expression<Func<TOwner, string>> expression; 

        PrintingConfig<TOwner> ISerializeConfig<TOwner, TType>
            .PrintingConfig => printingConfig;

        Expression<Func<TOwner, string>> ISerializeConfig<TOwner, TType> .Expression => expression;

        public SerializeConfig(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public SerializeConfig(PrintingConfig<TOwner> printingConfig, Expression<Func<TOwner, string>> expression)
        {
            this.printingConfig = printingConfig;
            this.expression = expression;
        }

        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            ((IPrintingConfig)printingConfig).AlternativeSerializersByType.Add(
                typeof(TType),
                new Func<object, string>(obj => func((TType)obj)));
            return printingConfig;
        }
    }

    public static class SerializeConfigExtension
    {
        public static PrintingConfig<TOwner> Using<TOwner>(this SerializeConfig<TOwner, double> config, CultureInfo culture)
        {
            return config.Printing<double>().Using(n => n.ToString(culture));
        }

        public static PrintingConfig<TOwner> Cut<TOwner>(this SerializeConfig<TOwner, string> config, int length)
        {
            return config
                .Printing(((ISerializeConfig<TOwner, string>)config).Expression)
                .Using(str => str.Substring(0, length));
        }
    }

    public interface ISerializeConfig<TOwner, TType>
    {
        PrintingConfig<TOwner> PrintingConfig { get; }
        Expression<Func<TOwner, string>> Expression { get; }
    }
}
