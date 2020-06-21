using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AtomExpression : Expression
    {
        public Expression Expression { get; }

        public AtomExpression(Expression expression)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            foreach (var result in this.Expression.Run(input, index))
            {
                yield return new Result(input, index, result.Length, this, new [] { result });
                yield break;
            }
        }
    }
}