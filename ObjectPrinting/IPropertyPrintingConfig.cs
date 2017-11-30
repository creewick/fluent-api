using System.Reflection;

namespace ObjectPrinting
{
    public interface IPropertyPrintingConfig<TOwner>
    {
        PrintingConfig<TOwner> PrintingConfig { get; }
        MemberInfo MemberInfo { get; }
    }
}
