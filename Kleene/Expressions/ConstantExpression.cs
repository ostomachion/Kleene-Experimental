using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConstantExpression<T> : Expression
    {
        public T Value { get; }

        public ConstantExpression(T value)
        {
            this.Value = value;
        }

        public override IEnumerable<Expression> Run()
        {
            yield return this;
        }
    }
}