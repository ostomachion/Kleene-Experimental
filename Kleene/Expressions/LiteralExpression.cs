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

        protected override bool InnerStep(out Result<T>? value)
        {
            if (this.Anchor is null)
            {
                value = new LiteralResult<T>(this.Item);
                return true;
            }
            else
            {
                this.Anchor.Step();
                if (this.Anchor.Result is AnyResult<T>)
                {
                    value = new LiteralResult<T>(this.Item);
                }
                else if (this.Anchor.Result is LiteralResult<T> result)
                {
                    value = result.Value.Equals(this.Item) ? result : null;
                }
                else
                {
                    value = null;
                }
                return this.Anchor.Done;
            }
        }

        protected override void InnerReset() { }
    }
}
