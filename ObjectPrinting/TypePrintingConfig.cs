using System;
using System.Linq.Expressions;

namespace ObjectPrinting
{
    public class TypePrintingConfig<TOwner, TType> : PrintingConfig<TOwner>, ITypePrintingConfig<TOwner>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        private readonly Expression<Func<TOwner, string>> expression; 

        PrintingConfig<TOwner> ITypePrintingConfig<TOwner>
            .PrintingConfig => printingConfig;

        Expression<Func<TOwner, string>> ITypePrintingConfig<TOwner> .Expression => expression;

        public TypePrintingConfig(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public TypePrintingConfig(PrintingConfig<TOwner> printingConfig, Expression<Func<TOwner, string>> expression)
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
}
