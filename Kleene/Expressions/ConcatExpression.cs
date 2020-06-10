using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConcatExpression<TIn, TOut> : VariadicExpression<TIn, TOut>
    {
        public ConcatExpression(IEnumerable<Expression<TIn, TOut>> expressions)
            : base(expressions) { }

        public override IEnumerable<IEnumerable<TOut>> Run(IEnumerable<TIn> input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            throw new NotImplementedException();
        }
    }
}