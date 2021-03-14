using System;
using System.Collections.Generic;

namespace Kleene
{
    public class NondeterministicRunnable<T> : NondeterministicObject<Runnable<T>>
    {
        public T Value { get; }

        public NondeterministicRunnable(T value)
        {
            Value = value;
        }

        public override IEnumerable<Runnable<T>> Collapse()
        {
            yield return new Runnable<T>(this.Value);
        }

        public override IEnumerable<NondeterministicObject<Runnable<T>>> Overlap(NondeterministicObject<Runnable<T>> other)
        {
            if (other is NondeterministicRunnable<T> runnable)
            {
                if (!EqualityComparer<T>.Default.Equals(this.Value, runnable.Value))
                    yield break;
                
                yield return new NondeterministicRunnable<T>(this.Value);
            }
            else
            {
                throw new ArgumentException("Argument type is not supported.", nameof(other));
            }
        }

        public override bool Equals(NondeterministicObject<Runnable<T>>? other) => this.Equals(other as object);

        public override bool Equals(object? obj)
        {
            return obj is NondeterministicRunnable<T> runnable &&
                   EqualityComparer<T>.Default.Equals(Value, runnable.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static implicit operator NondeterministicRunnable<T>(T value) => new(value);
    }
}