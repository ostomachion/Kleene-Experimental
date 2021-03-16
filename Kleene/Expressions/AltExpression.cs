using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<T> : Expression<T> where T : class
    {
        private int index = 0;

        public ReadOnlyCollection<Expression<T>> Expressions { get; }

        public AltExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(expressions));
        }

        protected override bool InnerStep(out T? value, Expression<T> anchor)
        {
            if (this.Expressions.Count == 0)
            {
                value = null;
                return true;
            }

            var expression = this.Expressions[this.index];
            expression.Step(anchor);
            value = expression.Value;
            if (expression.Done)
                this.index++;
            return this.index == this.Expressions.Count;
        }

        protected override void InnerReset()
        {
            foreach (var expression in this.Expressions)
            {
                expression.Reset();
            }
            this.index = 0;
        }
    }
}
