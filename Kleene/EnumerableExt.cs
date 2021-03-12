using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public static class EnumerableExt
    {
        public static IEnumerable<T> Yield<T>(T item) { yield return item; }
    }
}
