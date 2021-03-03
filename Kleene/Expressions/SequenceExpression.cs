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

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (!this.Expressions.Any())
            {
                yield return new Result(input, index, 0, this, Enumerable.Empty<Result>());
                yield break;
            }

            var stack = new Stack<IEnumerator<Result>>();
            stack.Push(this.Expressions.First().Run(input, index).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        yield return new Result(input, index, stack.Sum(x => x.Current.Length), this, stack.Reverse().Select(x => x.Current).ToArray());
                    }
                    else
                    {
                        stack.Push(this.Expressions.ElementAt(stack.Count).Run(input, index + stack.Sum(x => x.Current.Length)).GetEnumerator());
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