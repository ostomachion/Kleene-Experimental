using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression
    {
        public abstract IEnumerable<NondeterministicStructure?> Run();

        public IEnumerable<NondeterministicStructure?> Run(Structure? input)
        {
            return new ConjunctionExpression((Expression)input, this).Run();
        }

        public static explicit operator Expression(Structure? structure)
        {
            return structure is Structure
                ? new SequenceExpression(new [] {
                    new StructureExpression(structure.Name,
                        new SequenceExpression(structure.Children.Cast<StructureExpression>())),
                        (Expression)structure.NextSibling,
                    })
                : SequenceExpression.Empty;
        }
    }
}
