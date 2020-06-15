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
            if (this.Expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }
        
        public override IEnumerable<Structure> Run(Structure input)
        {
            foreach (var expression in this.Expressions)
            {
                foreach (var result in expression.Run(input))
                {
                    yield return result;
                }
            }
        }
    }
}