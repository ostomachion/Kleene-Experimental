using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Kleene
{
    public class LiteralExpression<T> : Expression<T> where T : class
    {
        public T Item { get; }

        public LiteralExpression(T item)
        {
            Item = item;
        }

        protected override bool InnerStep(out T? value, Expression<T> anchor)
        {
            // TODO: Check anchor.
            value = this.Item;
            return true;
        }

        protected override void InnerReset() { }
    }
}
