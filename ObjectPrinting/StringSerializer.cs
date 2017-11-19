using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public class StringSerializer<TOwner>
    {
        private readonly PrintingConfig<TOwner> printingConfig;

        public StringSerializer(PrintingConfig<TOwner> printingConfig)
        {
            this.printingConfig = printingConfig;
        }

        public PrintingConfig<TOwner> Cut(int length)
        {
            return printingConfig;
        }
    }
}
