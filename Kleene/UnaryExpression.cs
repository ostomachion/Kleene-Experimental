using System;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class UnaryExpression<TIn, TOut> : Expression<TIn, TOut>
    {
        public Expression<TIn, TOut> Expression { get; }
        public override IEnumerable<Expression<TIn, TOut>> Expressions { get { yield return this.Expression; } }

        public UnaryExpression(Expression<TIn, TOut> expression)
        {
            this.Expression = expression;
        }
    }
}
