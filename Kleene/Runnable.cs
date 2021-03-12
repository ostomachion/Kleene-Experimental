using System;
using System.Collections.Generic;

namespace Kleene
{
    public class Runnable<T> : IRunnable<Runnable<T>>
    {
        public T Value { get; }

        public Runnable(T value)
        {
            Value = value;
        }

        public Expression<Runnable<T>> ToExpression()
        {
            return new RunnableExpression<T>(this.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is Runnable<T> runnable &&
                   EqualityComparer<T>.Default.Equals(Value, runnable.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static implicit operator Runnable<T>(T value) => new(value);
    }

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

        public override IEnumerable<NondeterministicObject<Runnable<T>>?> Overlap(NondeterministicObject<Runnable<T>> other)
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

        public static implicit operator NondeterministicRunnable<T>(T value) => new(value);
    }

    public class RunnableExpression<T> : Expression<Runnable<T>>
    {
        public T Value { get; }

        public RunnableExpression(T value)
        {
            Value = value;
        }

        public override IEnumerable<NondeterministicObject<Runnable<T>>?> Run()
        {
            yield return new NondeterministicRunnable<T>(this.Value);
        }

        public static implicit operator RunnableExpression<T>(T value) => new(value);
    }
}