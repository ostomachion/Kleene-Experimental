using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T>
    where T : Expression<T>
    {
        public abstract IEnumerable<T> Run();
    }
}
