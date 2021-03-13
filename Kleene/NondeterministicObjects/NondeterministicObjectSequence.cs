using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class NondeterministicObjectSequence<T> : NondeterministicObject<ObjectSequence<T>> where T : IRunnable<T>
    {
        public NondeterministicObject<T> Head { get; }

        public IEnumerable<NondeterministicObject<ObjectSequence<T>>> Tail { get; }

        public NondeterministicObjectSequence(NondeterministicObject<T> head, IEnumerable<NondeterministicObject<ObjectSequence<T>>> tail)
        {
            Head = head;
            Tail = tail;
        }

        public override IEnumerable<ObjectSequence<T>> Collapse()
        {
            foreach (var head in Head.Collapse())
            {
                foreach (var tail in Tail.SelectMany(x => x.Collapse()))
                {
                    yield return new ObjectSequence<T>(new[] { head }.Concat(tail));
                }
            }
        }

        public override IEnumerable<NondeterministicObject<ObjectSequence<T>>> Overlap(NondeterministicObject<ObjectSequence<T>> other)
        {
            if (other is NondeterministicObjectSequence<T> sequence)
            {
                foreach (var head in NondeterministicObject<T>.Overlap(this.Head, sequence.Head))
                {
                    yield return new NondeterministicObjectSequence<T>(head, NondeterministicObject<ObjectSequence<T>>.Overlap(this.Tail, sequence.Tail));
                }
            }
            else if (other is NondeterministicEmptyObjectSequence<T>)
            {
                yield break;
            }
            else
            {
                throw new ArgumentException("Argument type is not supported.", nameof(other));
            }
        }
    }
}
