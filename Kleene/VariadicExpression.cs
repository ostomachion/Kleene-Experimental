using System;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class VariadicExpression<TIn, TOut> : Expression<TIn, TOut>
    {
        public override IEnumerable<Expression<TIn, TOut>> Expressions { get; }

        public VariadicExpression(IEnumerable<Expression<TIn, TOut>> expressions)
        {
            this.Expressions = expressions;
        }
    }
}
