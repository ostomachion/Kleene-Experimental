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
            if (anchor.Value is AnyResult<T>)
            {
                value = new RealResult<T>(this.Item);
            }
            else if (anchor.Value is RealResult<T> result)
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
