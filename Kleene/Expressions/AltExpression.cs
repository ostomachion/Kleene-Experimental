using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<T> : Expression<T>
        where T : Structure
    {
        public IEnumerable<Expression<T>> Expressions { get; }

        public AltExpression(IEnumerable<Expression<T>> expressions)
        {
            if (this.Expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }
        
        public override IEnumerable<T> Run()
        {
            foreach (var expression in this.Expressions)
            {
                foreach (var result in expression.Run())
                {
                    yield return result;
                }
            }
        }
    }
}