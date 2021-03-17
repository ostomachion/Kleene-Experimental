using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace Kleene
{
    public class SequenceExpression<T> : Expression<ImmutableList<T>> where T : class
    {
        public static readonly SequenceExpression<T> Empty = new(Enumerable.Empty<Expression<T>>());

        private int index = 0;

        public ImmutableList<Expression<T>> Expressions { get; }

        public SequenceExpression(IEnumerable<Expression<T>> expressions)
        {
            this.Expressions = expressions?.ToImmutableList() ?? throw new ArgumentNullException(nameof(expressions));

            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }
        }

        protected override bool InnerStep(out Result<ImmutableList<T>>? value, Expression<ImmutableList<T>> anchor)
        {
            // TODO: Use anchor.

            if (this.Expressions.Count == 0)
            {
                value = SequenceResult<T>.Empty;
                return true;
            }

            if (this.index == this.Expressions.Count)
            {
                // Backtrack.
                while (this.index-- != 0 && this.Expressions[this.index].Done)
                {
                    this.Expressions[this.index].Reset();
                }

                if (this.index == -1)
                {
                    value = null;
                    return true;
                }
            }

            var expression = this.Expressions[index];
            expression.Step(new AnyExpression<T>());
            if (expression.Result is null)
            {
                value = null;
            }
            else
            {
                this.index++;
                value = this.index == this.Expressions.Count ? new SequenceResult<T>(this.Expressions.Select(x => x.Result!)) : null;
            }

            return false;
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
