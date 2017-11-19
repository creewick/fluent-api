using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
    public interface INum<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);

        T Negate(T a);

        T Abs(T a);
        T Signum(T a);

        T FromInteger(int x);
    }
}
