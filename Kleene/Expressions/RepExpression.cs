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

        public override IEnumerable<IEnumerable<Structure>> Run(IEnumerable<Structure> input, int offset)
        {
            if (this.Order == Order.Lazy && this.Min == 0 || this.Max == 0)
            {
                yield return Enumerable.Empty<Structure>();
                if (this.Max == 0)
                {
                    yield break;
                }
            }

            var stack = new Stack<IEnumerator<IEnumerable<Structure>>>();
            stack.Push(this.Expression.Run(input, offset).GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Max)
                    {
                        yield return stack.Reverse().SelectMany(x => x.Current).ToList();
                    }
                    else
                    {
                        if (this.Order == Order.Lazy && stack.Count >= this.Min)
                        {
                            yield return stack.Reverse().SelectMany(x => x.Current).ToList();
                        }
                        var length = stack.Sum(x => x.Current.Count());
                        stack.Push(this.Expression.Run(input, offset + length).GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();

                    if (this.Order == Order.Greedy)
                    {
                        if (stack.Any() && stack.Count >= this.Min)
                        {
                            yield return stack.Reverse().SelectMany(x => x.Current).ToList();
                        }
                        else if (this.Min == 0)
                        {
                            yield return Enumerable.Empty<Structure>();
                        }
                    }
                }
            }
        }
    }
}