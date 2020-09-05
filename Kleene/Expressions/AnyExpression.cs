using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression : Expression
    {
        public AnyExpression() { }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            yield return new AnyStructure(EnumerableExt.Yield<NondeterministicStructure?>(null));
        }
    }
}