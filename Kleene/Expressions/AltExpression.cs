using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<T> : Expression<T>
    where T : Expression<T>
    {
        public IEnumerable<T> Expressions { get; }

        public AltExpression(IEnumerable<T> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions?.ToList() ?? throw new ArgumentNullException(nameof(expressions));
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