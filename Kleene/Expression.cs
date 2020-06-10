using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public abstract class Expression<TIn, TOut>
    {
        public abstract IEnumerable<Expression<TIn, TOut>> Expressions { get; }

        public IEnumerable<Result<TOut>> Run(IEnumerable<TIn> input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return RunAtOffset(input, 0);
        }

        internal abstract IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<TIn> input, int offset);

        internal static bool Consume(IEnumerable<TIn> input, int offset, TIn item)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Debug.Assert(offset >= 0 && offset <= input.Count());

            // At end of stream.
            if (offset == input.Count())
            {
                return false;
            }

            TIn current = input.ElementAt(offset);
            return input.ElementAt(offset)?.Equals(item) ?? item is null;
        }
    }
}
