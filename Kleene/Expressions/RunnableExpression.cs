using System.Collections.Generic;

namespace Kleene
{
    public class RunnableExpression<T> : Expression<Runnable<T>>
    {
        public T Value { get; }

        public RunnableExpression(T value)
        {
            Value = value;
        }

        public override IEnumerable<NondeterministicObject<Runnable<T>>> Run()
        {
            yield return new NondeterministicRunnable<T>(this.Value);
        }

        public static implicit operator RunnableExpression<T>(T value) => new(value);
    }
}