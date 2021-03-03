using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AllExpression : Expression
    {
        public Order Order { get; }

        public AllExpression(Order order = Order.Greedy)
        {
            this.Order = order;
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            switch (this.Order)
            {
                case Order.Greedy:
                    for (int i = input.Count(); i >= index; i--)
                    {
                        yield return new Result(input, index, i - index, this, Enumerable.Empty<Result>());
                    }
                    break;
                case Order.Lazy:
                    for (int i = index; i <= input.Count(); i++)
                    {
                        yield return new Result(input, index, i - index, this, Enumerable.Empty<Result>());
                    }
                    break;
            }
        }
    }
}