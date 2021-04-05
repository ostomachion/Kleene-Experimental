using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SetExpression<T> : Expression<ObjectSet<T>> where T : IRunnable<T>
    {
        public SequenceExpression<T> Expression { get; }

        public SetExpression(SequenceExpression<T> expression)
        {
            Expression = expression;
        }

        public override IEnumerable<NondeterministicObject<ObjectSet<T>>> Run()
        {
            foreach (var result in this.Expression.Run())
            {
                yield return NondeterministicObjectSet<T>.FromNondeterministicSequence(result);
            }
        }
    }
}
