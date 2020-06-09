using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class LiteralExpression<T> : NullaryExpression<T, T>
    {
        public T Value { get; }

        public LiteralExpression(T value)
        {
            this.Value = value;
        }

        public override IEnumerable<IEnumerable<T>> Run(IEnumerable<T> input)
        {
            if (input.Any() && input.First().Equals(this.Value))
            {
                yield return new [] { this.Value };
            }
        }
    }
}