using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectPrinting
{
    public interface IPrintingConfig
    {
        Dictionary<Type, Func<object, string>> AlternativeSerializersByType { get; }
        Dictionary<MemberInfo, Func<object, string>> AlternativeSerializersByName { get; }
        HashSet<Type> FinalTypes { get; }
        List<Type> ExcludedTypes { get; }
        List<MemberInfo> ExcludedFields { get; }
    }
}
