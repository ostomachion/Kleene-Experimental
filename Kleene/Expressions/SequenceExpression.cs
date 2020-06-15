using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression<T> : Expression<SequenceExpression<T>>
    where T : Expression<T>
    {
        public IEnumerable<Expression<T>> Expressions { get; }
        public SequenceExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            // Calling ToList to make sure they're enumerated.
            this.Expressions = expressions?.ToList() ?? throw new ArgumentNullException(nameof(expressions));
        }

        public override IEnumerable<SequenceExpression<T>> Run()
        {
            if (!this.Expressions.Any())
            {
                yield return new SequenceExpression<T>(Enumerable.Empty<T>());
                yield break;
            }

            var stack = new Stack<IEnumerator<T>>();
            stack.Push(this.Expressions.First().Run().GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return new SequenceExpression<T>(stack.Reverse().Select(x => x.Current));
                    }
                    else
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run().GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();
                }
            }
        }
    }
}