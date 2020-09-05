using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class RepExpression : Expression
    {
        public Expression Expression { get; }
        public RepExpression(Expression expression)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            return SequenceExpression.Concat(this.Expression.Run(), this.Run()).Concat(SequenceExpression.Empty.Run());
        }
    }
}