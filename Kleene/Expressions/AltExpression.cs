using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }

        public AltExpression(IEnumerable<Expression> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions?.ToList() ?? throw new ArgumentNullException(nameof(expressions));
        }
        
        public override IEnumerable<NondeterministicStructure?> Run()
        {
            return this.Expressions.SelectMany(x => x.Run());
        }
    }
}