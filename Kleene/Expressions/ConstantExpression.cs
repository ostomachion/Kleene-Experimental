using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConstantExpression<T> : Expression
    {
        public ConstantStructure<T> Value { get; }

        public ConstantExpression(T value)
        {
            this.Value = new ConstantStructure<T>(value);
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            if (index == input.Count())
                yield break;
                
            var structure = input.ElementAt(index);
            if (structure is ConstantStructure<T> constant && (constant.Value?.Equals(this.Value.Value) ?? this.Value is null))
            {
                yield return new Result(input, index, 1, this, Enumerable.Empty<Result>());
            }
        }
    }
}