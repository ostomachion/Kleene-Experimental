using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<TIn, TOut> : VariadicExpression<TIn, TOut>
    {
        public AltExpression(IEnumerable<Expression<TIn, TOut>> expressions)
            : base(expressions) { }

        public override IEnumerable<IEnumerable<TOut>> Run(IEnumerable<TIn> input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

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