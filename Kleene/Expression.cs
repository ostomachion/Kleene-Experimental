using System;
using System.Collections.Generic;

namespace Kleene
{
    public abstract class Expression<TIn, TOut>
    {
        public abstract IEnumerable<Expression<TIn, TOut>> Expressions { get; }

        // We will find out if this signature makes sense.
        public abstract IEnumerable<IEnumerable<TOut>> Run(IEnumerable<TIn> input);
    }
}
