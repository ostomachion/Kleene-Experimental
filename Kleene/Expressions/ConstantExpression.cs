using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConstantExpression<T> : Expression<ConstantExpression<T>>
    {
        public T Value { get; }

        public ConstantExpression(T value)
        {
            this.Value = value;
        }

        public override IEnumerable<ConstantExpression<T>> Run()
        {
            yield return this;
        }
    }
}