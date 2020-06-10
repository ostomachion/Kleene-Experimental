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

        public override IEnumerable<IEnumerable<TOut>> Run(IEnumerable<_> input)
        {
            yield return new [] { this.Value };
        }
    }
}