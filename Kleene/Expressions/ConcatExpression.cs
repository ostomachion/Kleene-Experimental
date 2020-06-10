using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConcatExpression<TIn, TOut> : VariadicExpression<TIn, TOut>
    {
        public ConcatExpression(IEnumerable<Expression<TIn, TOut>> expressions)
            : base(expressions) { }

        internal override IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<TIn> input, int offset)
        {
            if (!this.Expressions.Any())
            {
                yield return new Result<TOut>(offset);
                yield break;
            }

            var stack = new Stack<IEnumerator<Result<TOut>>>();
            stack.Push(this.Expressions.First().RunAtOffset(input, offset).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Expressions.Count())
                    {
                        var length = stack.Sum(x => x.Current.Length);
                        var output = stack.Reverse().SelectMany(x => x.Current.Output).ToList();
                        yield return new Result<TOut>(offset, length, output);
                    }
                    else
                    {
                        var length = stack.Sum(x => x.Current.Length);
                        stack.Push(this.Expressions.ElementAt(stack.Count).RunAtOffset(input, offset + length).GetEnumerator());
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