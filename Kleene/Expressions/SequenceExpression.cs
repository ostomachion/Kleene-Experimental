using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression<T> : Expression<SequenceStructure<T>>
        where T : Structure
    {
        public IEnumerable<Expression<T>> Expressions { get; }
        public SequenceExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }

        internal override IEnumerable<SequenceStructure<T>> Run(SequenceStructure<T>? input)
        {
            if (!this.Expressions.Any())
            {
                yield return new SequenceStructure<T>(Enumerable.Empty<T>());
                yield break;
            }

            var stack = new Stack<IEnumerator<T>>();
            if (input is null || input.Current is T)
            {
                stack.Push(this.Expressions.First().Run(input?.Current).GetEnumerator());
            }

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current), stack.Count);
                    }
                    else if (input is null || input.Current is T)
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(input?.Current).GetEnumerator());
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