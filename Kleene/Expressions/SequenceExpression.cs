using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression : Expression
    {
        public IEnumerable<Expression> Expressions { get; }
        public SequenceExpression(IEnumerable<Expression> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            // Calling ToList to make sure they're enumerated.
            this.Expressions = expressions?.ToList() ?? throw new ArgumentNullException(nameof(expressions));
        }

        public override IEnumerable<Expression> Run()
        {
            if (!this.Expressions.Any())
            {
                yield return new SequenceExpression(Enumerable.Empty<Expression>());
                yield break;
            }

            var stack = new Stack<IEnumerator<Expression>>();
            stack.Push(this.Expressions.First().Run().GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return new SequenceExpression(stack.Reverse().Select(x => x.Current));
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