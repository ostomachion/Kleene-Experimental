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
                    yield return new ObjectSet<T>(new[] { head }.Union(tail));
                }
            }
        }

        public override IEnumerable<NondeterministicObject<ObjectSet<T>>> Overlap(NondeterministicObject<ObjectSet<T>> other)
        {
            if (other is NondeterministicObjectSet<T> set)
            {
                // Evaluate until set.Head is found
                // Then return head and overlap remaining???

                // this.Head and set.Head
                foreach (var head in NondeterministicObject<T>.Overlap(this.Head, set.Head))
                {
                    yield return new NondeterministicObjectSet<T>(head, Overlap(this.Tail, set.Tail));
                }

                // this.Head and set.Tail
                foreach (var tail in set.Tail)
                {
                    if (tail is NondeterministicEmptyObjectSet<T>)
                    {
                        continue;
                    }
                    else if (tail is NondeterministicObjectSet<T> tailSet)
                    {
                        foreach (var head in NondeterministicObject<T>.Overlap(this.Head, tailSet.Head))
                        {
                            // Still not convinced this is always right.
                            yield return new NondeterministicObjectSet<T>(head, Overlap(this.Tail, EnumerableExtensions.Yield(new NondeterministicObjectSet<T>(set.Head, tailSet.Tail))));
                        }
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
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
