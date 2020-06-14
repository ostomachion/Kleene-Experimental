using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T>
        where T : Structure
    {
        public IEnumerable<T> Run() => Run(null);
        internal abstract IEnumerable<T> Run(T? input);
    }
}
