using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConstantExpression<T> : Expression<T>
        where T : Structure
    {
        public T Value { get; }

        public ConstantExpression(T value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<T> Run()
        {
            yield return this.Value;
        }
    }
}