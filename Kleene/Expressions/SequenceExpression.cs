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

        public override IEnumerable<StructurePointer<TIn>> Run<TIn>(StructurePointer<TIn> input)
        {
            if (!this.Expressions.Any())
            {
                yield return null; // TODO:
                yield break;
            }

            var stack = new Stack<IEnumerator<StructurePointer<TIn>>>();
            stack.Push(this.Expressions.First().Run(input).GetEnumerator()); // TODO: Get latest input.

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return null; // TODO:
                    }
                    else
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(input).GetEnumerator()); // TODO: Get latest input.
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