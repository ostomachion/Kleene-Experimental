using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : IRunnable<T>
    {
        public abstract IEnumerable<NondeterministicObject<T>?> Run();

        public IEnumerable<NondeterministicObject<T>?> Run(T input)
        {
            return new ConjunctionExpression<T>(input.ToExpression(), this).Run();
        }
    }
}
