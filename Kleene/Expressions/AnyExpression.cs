using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression<T> : Expression<T> where T : class
    {
        protected override bool InnerStep(out T? value)
        {
            throw new InvalidOperationException();
        }
    }
}
