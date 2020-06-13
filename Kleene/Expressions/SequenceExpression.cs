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
            this.Expressions = expressions;
        }

        public override IEnumerable<SequenceStructure<T>> Run()
        {
            if (!this.Expressions.Any())
            {
                yield return new SequenceStructure<T>(Enumerable.Empty<T>());
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
                        yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current));
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