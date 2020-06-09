using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class CharExpression : NullaryExpression<char, char>
    {
        public char Value { get; }

        public CharExpression(char value)
        {
            this.Value = value;
        }

        public override IEnumerable<IEnumerable<char>> Run(IEnumerable<char> input)
        {
            if (input.Any() && input.First() == this.Value)
            {
                yield return new [] { this.Value };
            }
        }
    }
}