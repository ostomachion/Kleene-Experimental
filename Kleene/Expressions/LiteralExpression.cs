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

        protected override bool InnerStep(out T? value)
        {
            value = this.Item;
            return true;
        }

        protected override void InnerReset() { }
    }
}
