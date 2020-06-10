using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<TIn, TOut>
    {
        public abstract IEnumerable<Expression<TIn, TOut>> Expressions { get; }

        public IEnumerable<Result<TOut>> Run(IEnumerable<TIn> input) => RunAtOffset(input, 0);

        internal abstract IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<TIn> input, int offset);

        internal static Result<TOut>? Consume(IEnumerable<TIn> input, int offset, TIn item)
        {
            Debug.Assert(offset >= 0 && offset <= input.Count());

            // At end of stream.
            if (offset == input.Count())
            {
                return null;
            }

            TIn current = input.ElementAt(offset);
            if (current?.Equals(item) ?? item is null)
            {
                return new Result<TOut>(offset, 1, Enumerable.Empty<TOut>());
            }

            return null;
        }
    }
}
