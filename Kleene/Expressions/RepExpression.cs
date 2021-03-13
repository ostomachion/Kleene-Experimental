using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class RepExpression<T> : Expression<ObjectSequence<T>> where T : IRunnable<T>
    {
        public Expression<T> Expression { get; }
        public RepExpression(Expression<T> expression)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override IEnumerable<NondeterministicObject<ObjectSequence<T>>> Run()
        {
            // p* := pp*|1
            foreach (var result in SequenceExpression<T>.Concat(this.Expression.Run(), this.Run()).Concat(SequenceExpression<T>.Empty.Run()))
            {
                yield return result;
            }
        }
    }
}
