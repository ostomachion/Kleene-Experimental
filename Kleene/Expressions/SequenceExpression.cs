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

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int index)
        {
            if (!this.Expressions.Any())
            {
                yield return Enumerable.Empty<Structure>();
                yield break;
            }

            var stack = new Stack<IEnumerator<IEnumerable<Structure>>>();
            stack.Push(this.Expressions.First().Run(input, index).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return stack.Reverse().SelectMany(x => x.Current).ToList();
                    }
                    else
                    {
                        var length = stack.Sum(x => x.Current.Count());
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(input, index + length).GetEnumerator());
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