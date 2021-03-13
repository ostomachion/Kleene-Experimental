using System;
using System.Collections.Generic;

namespace Kleene
{
    public class AnyObject<T> : NondeterministicObject<T> where T : IRunnable<T>
    {
        public override IEnumerable<T> Collapse() => throw new InvalidOperationException();

        public override IEnumerable<NondeterministicObject<T>> Overlap(NondeterministicObject<T> other)
        {
            yield return other;
        }
    }
}
