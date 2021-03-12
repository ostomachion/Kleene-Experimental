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
}