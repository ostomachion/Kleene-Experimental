using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public enum Order { Lazy, Greedy }

    public class RepExpression<T> : Expression<SequenceStructure<T>>
        where T : Structure
    {
        public Order Order { get; }
        public int Min { get; }
        public int Max { get; }
        public Expression<T> Expression { get; }

        public RepExpression(Expression<T> expression, Order order = Order.Greedy, int min = 0, int max = -1)
        {
            this.Order = order;
            this.Min = min;
            this.Max = max;
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override IEnumerable<SequenceStructure<T>> Run()
        {
            if (this.Order == Order.Lazy && this.Min == 0 || this.Max == 0)
            {
                yield return new SequenceStructure<T>(Enumerable.Empty<T>());
                if (this.Max == 0)
                {
                    yield break;
                }
            }

            var stack = new Stack<IEnumerator<T>>();
            stack.Push(this.Expression.Run().GetEnumerator());

            while (stack.Any())
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Count == this.Max)
                    {
                        yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current));
                    }
                    else
                    {
                        if (this.Order == Order.Lazy && stack.Count >= this.Min)
                        {
                            yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current));
                        }
                        stack.Push(this.Expression.Run().GetEnumerator());
                    }
                }
                else
                {
                    stack.Pop();

                    if (this.Order == Order.Greedy)
                    {
                        if (stack.Any() && stack.Count >= this.Min)
                        {
                            yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current));
                        }
                        else if (this.Min == 0)
                        {
                            yield return new SequenceStructure<T>(stack.Reverse().Select(x => x.Current));
                        }
                    }
                }
            }
        }
    }
}