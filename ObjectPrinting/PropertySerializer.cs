using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public class PropertySerializer<TOwner, TType>
    {
        private readonly PrintingConfig<TOwner> printingConfig;
        private readonly MemberInfo memberInfo;

        public PropertySerializer(PrintingConfig<TOwner> printingConfig, MemberInfo memberInfo)
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
