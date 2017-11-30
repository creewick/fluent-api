using System;
using System.Globalization;

namespace ObjectPrinting
{
    public static class TypePrintingConfigExtension
    {
        public static PrintingConfig<TOwner> Using<TOwner>(this TypePrintingConfig<TOwner, double> config, CultureInfo culture)
        {
            return config.Using(n => n.ToString(culture));
        }

        public static PrintingConfig<TOwner> Cut<TOwner>(this TypePrintingConfig<TOwner, string> config, int length)
        {
            return config
                .Printing<string>()
                .Using(str => str.Substring(0, Math.Min(length, str.Length)));
        }
    }
}
