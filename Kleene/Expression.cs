using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression
    {
        public abstract IEnumerable<NondeterministicStructure?> Run();

        public IEnumerable<NondeterministicStructure?> Run(Structure input)
        {
            return new ConjunctionExpression((StructureExpression)input, this).Run();
        }
    }
}
