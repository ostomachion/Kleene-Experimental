using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression
    {
        public abstract IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index);
    }
}
