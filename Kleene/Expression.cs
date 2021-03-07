using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : IRunnable<T>
    {
        public abstract IEnumerable<NondeterministicStructure?> Run();

        public IEnumerable<NondeterministicStructure?> Run(T input)
        {
            return new ConjunctionExpression<T>(input.ToExpression(), this).Run();
        }
    }
}
