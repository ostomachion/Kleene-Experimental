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
            if (Expression<TIn, _>.Consume(input, offset, this.Value))
            {
                yield return new Result<_>(offset, 1);
            }
        }
    }
}