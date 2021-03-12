using System.Collections.Generic;

namespace Kleene
{
    // TODO: Can this be replaced with just null?
    public class NondeterministicEmptyObjectSequence<T> : NondeterministicObject<ObjectSequence<T>> where T : IRunnable<T>
    {
        public override IEnumerable<ObjectSequence<T>> Collapse()
        {
            yield return ObjectSequence<T>.Empty;
        }

        public override IEnumerable<NondeterministicObject<ObjectSequence<T>>?> Overlap(NondeterministicObject<ObjectSequence<T>> other)
        {
            if (other is NondeterministicEmptyObjectSequence<T>)
            {
                yield return this;
            }
            else
            {
                throw new System.ArgumentException("Argument type is not supported.", nameof(other));
            }
        }
    }
}
