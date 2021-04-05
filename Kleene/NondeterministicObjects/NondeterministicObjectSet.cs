using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class NondeterministicObjectSet<T> : NondeterministicObject<ObjectSet<T>> where T : IRunnable<T>
    {
        public NondeterministicObject<T> Head { get; }

        public IEnumerable<NondeterministicObject<ObjectSet<T>>> Tail { get; }

        public NondeterministicObjectSet(NondeterministicObject<T> head, IEnumerable<NondeterministicObject<ObjectSet<T>>> tail)
        {
            Head = head;
            Tail = tail;
        }

        public static NondeterministicObject<ObjectSet<T>> FromNondeterministicSequence(NondeterministicObject<ObjectSequence<T>> sequence)
        {
            if (sequence is NondeterministicEmptyObjectSequence<T>)
            {
                return new NondeterministicEmptyObjectSet<T>();
            }
            else if (sequence is NondeterministicObjectSequence<T> cons)
            {
                return new NondeterministicObjectSet<T>(cons.Head, cons.Tail.Select(FromNondeterministicSequence));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public override IEnumerable<ObjectSet<T>> Collapse()
        {
            foreach (var head in Head.Collapse())
            {
                foreach (var tail in Tail.SelectMany(x => x.Collapse()))
                {
                    yield return new ObjectSet<T>(new[] { head }.Intersect(tail));
                }
            }
        }

        public override IEnumerable<NondeterministicObject<ObjectSet<T>>> Overlap(NondeterministicObject<ObjectSet<T>> other)
        {
            if (other is NondeterministicObjectSet<T> set)
            {
                throw new NotImplementedException();

                // TODO:
                // Evaluate until other.Head is found
                // Then return head and overlap remaining???

                // Try with head.
                foreach (var head in NondeterministicObject<T>.Overlap(this.Head, set.Head))
                {
                    yield return new NondeterministicObjectSet<T>(head, Overlap(this.Tail, set.Tail));
                }

                // Try with next item.
            }
            else if (other is NondeterministicEmptyObjectSet<T>)
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
