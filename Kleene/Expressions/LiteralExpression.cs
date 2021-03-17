using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Kleene
{
    public class LiteralExpression<T> : Expression<T> where T : notnull
    {
        public T Item { get; }

        public LiteralExpression(T item)
        {
            Item = item;
        }

        protected override bool InnerStep(out Result<T>? value, Expression<T> anchor)
        {
            anchor.Step(new AnyExpression<T>());
            if (anchor.Result is AnyResult<T>)
            {
                value = new LiteralResult<T>(this.Item);
            }
            else if (anchor.Result is LiteralResult<T> result)
            {
                value = result.Value.Equals(this.Item) ? result : null;
            }
            else
            {
                value = null;
            }
            return anchor.Done;
        }

        protected override void InnerReset() { }
    }
}
