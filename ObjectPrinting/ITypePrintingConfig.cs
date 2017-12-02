using System;
using System.Linq.Expressions;

namespace ObjectPrinting
{
    public interface ITypePrintingConfig<TOwner>
    {
        PrintingConfig<TOwner> PrintingConfig { get; }
    }
}
