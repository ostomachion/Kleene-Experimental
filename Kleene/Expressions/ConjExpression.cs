using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class ConjExpression<T> : Expression<T>
        where T : Structure
    {
        public IEnumerable<Expression<T>> Expressions { get; }

        public ConjExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
        }
        
        public override IEnumerable<T> Run()
        {
            throw new NotImplementedException();
        }
    }
}