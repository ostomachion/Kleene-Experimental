using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public abstract class VariadicExpression<TIn, TOut> : Expression<TIn, TOut>
    {
        public override IEnumerable<Expression<TIn, TOut>> Expressions { get; }

        public VariadicExpression(IEnumerable<Expression<TIn, TOut>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException(nameof(expressions));
            }

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }
    }
}
