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

        internal override IEnumerable<Result<T>> RunAtOffset(IEnumerable<T> input, int offset)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (Expression<T, T>.Consume(input, offset, this.Value) is Result<T> result)
            {
                yield return new Result<T>(offset, result.Length, new [] { this.Value });
            }
        }
    }
}