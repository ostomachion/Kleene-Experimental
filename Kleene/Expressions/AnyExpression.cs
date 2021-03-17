using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AnyExpression<T> : Expression<T> where T : notnull
    {
        protected override bool InnerStep(out Result<T>? value, Expression<T> anchor)
        {
            if (anchor is AnyExpression<T>)
            {
                value = new AnyResult<T>();
                return true;
            }
            else
            {
                anchor.Step(new AnyExpression<T>());
                value = anchor.Value;
                return anchor.Done;
            }
        }

        protected override void InnerReset() { }
    }
}
