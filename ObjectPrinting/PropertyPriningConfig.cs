using System;
using System.Reflection;

namespace ObjectPrinting
{
    public class PropertyPriningConfig<TOwner, TType> : IPropertyPrintingConfig<TOwner>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        private readonly MemberInfo memberInfo;

        PrintingConfig<TOwner> IPropertyPrintingConfig<TOwner>
            .PrintingConfig => printingConfig;
        MemberInfo IPropertyPrintingConfig<TOwner>
            .MemberInfo => memberInfo;

        public PropertyPriningConfig(PrintingConfig<TOwner> printingConfig, MemberInfo memberInfo)
        {
            this.printingConfig = printingConfig;
            this.memberInfo = memberInfo;
        }

        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            ((IPrintingConfig)printingConfig).AlternativeSerializersByName.Add(
                memberInfo, 
                obj => func((TType)obj));
            return printingConfig;
        }
    }
}
