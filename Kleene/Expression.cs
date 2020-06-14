using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression
    {
        public abstract IEnumerable<StructurePointer<TIn>> Run<TIn>(StructurePointer<TIn> input) where TIn : Structure;
    }
}
