using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public abstract class NondeterministicObject<T> : IEquatable<NondeterministicObject<T>> where T : IRunnable<T>
    {
        public abstract IEnumerable<T> Collapse();

        public abstract IEnumerable<NondeterministicObject<T>> Overlap(NondeterministicObject<T> other);

        public static IEnumerable<NondeterministicObject<T>> Overlap(NondeterministicObject<T> left, NondeterministicObject<T> right)
        {
            if (left is AnyObject<T>)
            {
                yield return right;
            }
            else if (right is AnyObject<T>)
            {
                yield return left;
            }
            else
            {
                foreach (var item in left.Overlap(right))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<NondeterministicObject<T>> Overlap(IEnumerable<NondeterministicObject<T>> left, IEnumerable<NondeterministicObject<T>> right)
        {
            foreach (var l in left)
            {
                foreach (var r in right)
                {
                    foreach (var overlap in Overlap(l, r))
                    {
                        yield return overlap;
                    }
                }
            }
        }

        public override abstract bool Equals(object? obj);

        public override abstract int GetHashCode();

        public abstract bool Equals(NondeterministicObject<T>? other);
    }
}
