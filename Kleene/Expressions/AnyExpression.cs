using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression<T> : Expression<T> where T : class
    {
        protected override bool InnerStep(out T? value, Expression<T> anchor)
        {
            throw new InvalidOperationException();
        }

        protected override void InnerReset() { }
    }
}
