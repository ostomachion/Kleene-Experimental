using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class NondeterministicEmptyObjectSet<T> : NondeterministicObject<ObjectSet<T>> where T : IRunnable<T>
    {
        public override IEnumerable<ObjectSet<T>> Collapse()
        {
            yield return ObjectSet<T>.Empty;
        }

        public override IEnumerable<NondeterministicObject<ObjectSet<T>>> Overlap(NondeterministicObject<ObjectSet<T>> other)
        {
            if (other is NondeterministicEmptyObjectSet<T>)
            {
                yield return this;
            }
            else if (other is NondeterministicObjectSet<T>)
            {
                yield break;
            }
            else
            {
                throw new System.ArgumentException("Argument type is not supported.", nameof(other));
            }
        }
    }
}
