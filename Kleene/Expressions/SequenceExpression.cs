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

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }

        public override IEnumerable<Structure> Run(Structure input)
        {
            if (!this.Expressions.Any())
            {
                yield return input;
                yield break;
            }

            var stack = new Stack<IEnumerator<Structure>>();
            stack.Push(this.Expressions.First().Run(input).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return stack.Peek().Current;
                    }
                    else
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(stack.Peek().Current).GetEnumerator());
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