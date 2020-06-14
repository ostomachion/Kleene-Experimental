using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression
    {
        public abstract IEnumerable<TIn> Run<TIn>(TIn input) where TIn : Structure;
    }
}
