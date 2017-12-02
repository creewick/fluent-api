using System;
using System.Linq.Expressions;

namespace ObjectPrinting
{
    public class TypePrintingConfig<TOwner, TType> : ITypePrintingConfig<TOwner>
    {
        private readonly PrintingConfig<TOwner> printingConfig;

        PrintingConfig<TOwner> ITypePrintingConfig<TOwner>
            .PrintingConfig => printingConfig;

        public TypePrintingConfig(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            ((IPrintingConfig)printingConfig).AlternativeSerializersByType.Add(
                typeof(TType),
                obj => func((TType)obj));
            return printingConfig;
        }
    }
}
