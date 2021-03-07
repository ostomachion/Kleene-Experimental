using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression<T> : Expression<ISequencable<T>> where T : IRunnable<T>
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

        public override IEnumerable<NondeterministicObject<ISequencable<T>>> Run()
        {
            // TODO: This can probably be a lot more efficient.

            if (!this.Expressions.Any())
            {
                return Enumerable.Empty<NondeterministicObject<ISequencable<T>>>();
            }

            var head = this.Expressions.First().Run();
            var tail = new SequenceExpression<T>(this.Expressions.Skip(1)).Run();

            return Concat(head, tail);
        }

        internal static IEnumerable<NondeterministicObject<ISequencable<T>>> Concat(IEnumerable<NondeterministicObject<T>> head, IEnumerable<NondeterministicObject<ISequencable<T>>> tail)
        {
            return head.SelectMany(x =>
                x is null ? tail :
                x is NondeterministicStructure named ? (IEnumerable<NondeterministicObject<T>?>)EnumerableExt.Yield(new NondeterministicStructure(named.Name, named.FirstChild, Concat(named.NextSibling, tail))) :
                x is AnyStructure<T> any ? EnumerableExt.Yield(new AnyStructure<T>(Concat(any.NextSibling, tail))) :
                throw new NotImplementedException());
        }
    }
}