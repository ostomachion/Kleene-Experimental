using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConstantExpression<T> : Expression
    {
        public ConstantStructure<T> Value { get; }

        public ConstantExpression(ConstantStructure<T> value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<StructurePointer<TIn>> Run<TIn>(StructurePointer<TIn> input)
        {
            Structure current = input.GetCurrent();
            if (current is ConstantStructure<T> constant && constant.Value is { } && constant.Value.Equals(this.Value.Value))
            {
                yield return input.Advance();
            }
        }
    }
}