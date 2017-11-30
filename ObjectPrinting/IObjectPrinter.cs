using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    interface IObjectPrinter
    {
        string PrintToString(IPrintingConfig config, object obj, int height);
    }
}
