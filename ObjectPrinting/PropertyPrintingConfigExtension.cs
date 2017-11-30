using System;

namespace ObjectPrinting
{
    public static class PropertyPrintingConfigExtension
    {
        public static PrintingConfig<TOwner> Cut<TOwner>(this PropertyPriningConfig<TOwner, string> config, int length)
        {
            ((IPrintingConfig)((IPropertyPrintingConfig<TOwner>)config).PrintingConfig).AlternativeSerializersByName.Add(
                ((IPropertyPrintingConfig<TOwner>)config).MemberInfo,
                obj => ((string)obj).Substring(0, Math.Min(length, ((string)obj).Length)));
            return ((IPropertyPrintingConfig<TOwner>)config).PrintingConfig;
        }
    }
}
