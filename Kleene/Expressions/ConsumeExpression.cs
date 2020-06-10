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

        public override IEnumerable<IEnumerable<_>> Run(IEnumerable<TIn> input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Any() && (input.First()?.Equals(this.Value) ?? this.Value is null))
            {
                yield return Enumerable.Empty<_>();
            }
        }
    }
}