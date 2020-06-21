using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class RepExpression : Expression
    {
        public Expression Expression { get; }
        public Order Order { get; }
        public int Min { get; }
        public int Max { get; }

        public RepExpression(Expression expression, Order order = Order.Greedy, int min = 0, int max = -1)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            this.Order = order;
            this.Min = min;
            this.Max = max;
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (this.Order == Order.Lazy && this.Min == 0 || this.Max == 0)
            {
                yield return new Result(input, index, 0, this, Enumerable.Empty<Result>());
                if (this.Max == 0)
                {
                    yield break;
                }
            }

            var stack = new Stack<IEnumerator<Result>>();
            stack.Push(this.Expression.Run(input, index).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Max)
                    {
                        yield return new Result(input, index, stack.Sum(x => x.Current.Length), this, stack.Reverse().Select(x => x.Current).ToArray());
                    }
                    else
                    {
                        if (this.Order == Order.Lazy && stack.Count >= this.Min)
                        {
                            yield return new Result(input, index, stack.Sum(x => x.Current.Length), this, stack.Reverse().Select(x => x.Current).ToArray());
                        }
                        stack.Push(this.Expression.Run(input, index + stack.Sum(x => x.Current.Length)).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();

                    if (this.Order == Order.Greedy)
                    {
                        if (stack.Any() && stack.Count >= this.Min)
                        {
                            yield return new Result(input, index, stack.Sum(x => x.Current.Length), this, stack.Reverse().Select(x => x.Current).ToArray());
                        }
                        else if (this.Min == 0)
                        {
                            yield return new Result(input, index, 0, this, Enumerable.Empty<Result>());
                        }
                    }
                }
            }
        }
    }
}