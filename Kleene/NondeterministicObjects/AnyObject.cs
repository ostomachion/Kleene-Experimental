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

        public override bool Equals(NondeterministicObject<T>? other) => this.Equals(other as object);

        public override string? ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is AnyObject<T>;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
