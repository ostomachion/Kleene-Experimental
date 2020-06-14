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

        internal override IEnumerable<T> Run(T? input)
        {
            #warning TODO:
            throw new NotImplementedException();
        }
    }
}