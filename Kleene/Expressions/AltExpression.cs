using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<TIn, TOut> : VariadicExpression<TIn, TOut>
    {
        public AltExpression(IEnumerable<Expression<TIn, TOut>> expressions)
            : base(expressions) { }

        internal override IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<TIn> input, int offset)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            foreach (var expression in this.Expressions)
            {
                foreach (var result in expression.RunAtOffset(input, offset))
                {
                    yield return result;
                }
            }
        }
    }
}