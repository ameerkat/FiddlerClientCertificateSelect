using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerClientCertificateSelect
{
    public class FuncComparer<T> : IComparer<T>
    {
        Func<T, T, int> func;
        public FuncComparer(Func<T, T, int> func)
        {
            this.func = func;
        }

        public int Compare(T x, T y)
        {
            return func(x, y);
        }
    }
}
