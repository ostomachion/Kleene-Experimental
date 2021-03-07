using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression<T> : Expression<T> where T : IRunnable<T>
    {
        public override IEnumerable<NondeterministicStructure?> Run()
        {
            yield return new AnyStructure();
        }
    }
}