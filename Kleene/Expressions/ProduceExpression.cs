using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ProduceExpression<_, TOut> : NullaryExpression<_, TOut>
    {
        public TOut Value { get; }

        public ProduceExpression(TOut value)
        {
            this.Value = value;
        }

        internal override IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<_> input, int offset)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            yield return new Result<TOut>(offset, 0, new [] { this.Value });
        }
    }
}