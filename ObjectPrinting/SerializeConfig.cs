using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TType>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        
        PrintingConfig<TOwner> ISerializeConfig<TOwner, TType>
            .PrintingConfig => printingConfig;

        public SerializeConfig(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            return printingConfig;
        }
    }

    public static class SerializeConfigExtension
    {
        public static PrintingConfig<TOwner> Using<TOwner>(this SerializeConfig<TOwner, double> config, CultureInfo culture)
        {
            return ((ISerializeConfig<TOwner, double>)config).PrintingConfig;
        }
    }

    public interface ISerializeConfig<TOwner, TType>
    {
        PrintingConfig<TOwner> PrintingConfig {get;}
    }
}
