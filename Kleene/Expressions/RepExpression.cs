using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public enum Order { Lazy, Greedy }
    public class RepExpression<TIn, TOut> : UnaryExpression<TIn, TOut>
    {
        public Order Order { get; }
        public int Min { get; }
        public int Max { get; }

        public RepExpression(Expression<TIn, TOut> expression, Order order = Order.Greedy, int min = 0, int max = -1)
            : base(expression)
        {
            this.Order = order;
            this.Min = min;
            this.Max = max;
        }

        internal override IEnumerable<Result<TOut>> RunAtOffset(IEnumerable<TIn> input, int offset)
        {
            // TODO: This can probably be simplified.
            if (this.Order == Order.Lazy && this.Min == 0 || this.Max == 0)
            {
                yield return new Result<TOut>(offset);
                if (this.Max == 0)
                {
                    yield break;
                }
            }

            var stack = new Stack<IEnumerator<Result<TOut>>>();
            stack.Push(this.Expression.RunAtOffset(input, offset).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    var length = stack.Sum(x => x.Current.Length);
                    if (stack.Count == this.Max)
                    {
                        var output = stack.Reverse().SelectMany(x => x.Current.Output).ToList();
                        yield return new Result<TOut>(offset, length, output);
                    }
                    else
                    {
                        if (this.Order == Order.Lazy && stack.Count >= this.Min)
                        {
                            var output = stack.Reverse().SelectMany(x => x.Current.Output).ToList();
                            yield return new Result<TOut>(offset, length, output);
                        }
                        stack.Push(this.Expression.RunAtOffset(input, offset + length).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();

                    if (this.Order == Order.Greedy)
                    {
                        if (stack.Any() && stack.Count >= this.Min)
                        {
                            var length = stack.Sum(x => x.Current.Length);
                            var output = stack.Reverse().SelectMany(x => x.Current.Output).ToList();
                            yield return new Result<TOut>(offset, length, output);
                        }
                        else if (this.Min == 0)
                        {
                            yield return new Result<TOut>(offset);
                        }
                    }
                }
            }
        }
    }
}