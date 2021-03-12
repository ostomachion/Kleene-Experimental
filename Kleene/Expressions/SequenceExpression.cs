using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression<T> : Expression<ObjectSequence<T>> where T : IRunnable<T>
    {
        public static readonly SequenceExpression<T> Empty = new(Enumerable.Empty<Expression<T>>());

        public IEnumerable<Expression<T>> Expressions { get; }

        public SequenceExpression(IEnumerable<Expression<T>> expressions)
        {
            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));

            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }
        }

        public override IEnumerable<NondeterministicObject<ObjectSequence<T>>?> Run()
        {
            if (!this.Expressions.Any())
            {
                return EnumerableExtensions.Yield(new NondeterministicEmptyObjectSequence<T>());
            }

            var head = this.Expressions.First().Run();
            var tail = new SequenceExpression<T>(this.Expressions.Skip(1)).Run();

            return Concat(head, tail);
        }

        internal static IEnumerable<NondeterministicObject<ObjectSequence<T>>?> Concat(IEnumerable<NondeterministicObject<T>?> head, IEnumerable<NondeterministicObject<ObjectSequence<T>>?> tail)
        {
            return head.Select(x => x is null ? null : new NondeterministicObjectSequence<T>(x, tail));
        }
    }
}
