using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConsumeExpression<TIn, _> : NullaryExpression<TIn, _>
    {
        public TIn Value { get; }

        public ConsumeExpression(TIn value)
        {
            this.Value = value;
        }

        internal override IEnumerable<Result<_>> RunAtOffset(IEnumerable<TIn> input, int offset)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (Expression<TIn, _>.Consume(input, offset, this.Value) is Result<_> result)
            {
                yield return new Result<_>(offset, result.Length);
            }
        }
    }
}