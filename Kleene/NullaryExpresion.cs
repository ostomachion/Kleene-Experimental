using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public abstract class NullaryExpression<TIn, TOut> : Expression<TIn, TOut>
    {
        public override IEnumerable<Expression<TIn, TOut>> Expressions => Enumerable.Empty<Expression<TIn, TOut>>();
    }
}
