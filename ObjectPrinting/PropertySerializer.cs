using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public class PropertySerializer<TOwner, TType>
    {
        private readonly PrintingConfig<TOwner> printingConfig;

        public PropertySerializer(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            return printingConfig;
        }
    }
}
