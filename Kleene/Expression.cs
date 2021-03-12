using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<T> where T : IRunnable<T>
    {
        public abstract IEnumerable<NondeterministicObject<T>?> Run();

        public IEnumerable<T> Generate()
        {
            return this.Run().OfType<NondeterministicObject<T>>().SelectMany(x => x.Collapse());
        }

        public IEnumerable<NondeterministicObject<T>?> Run(T input)
        {
            return new ConjunctionExpression<T>(input.ToExpression(), this).Run();
        }
    }
}
