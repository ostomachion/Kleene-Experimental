using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class AltExpression<T> : Expression<T> where T : IRunnable<T>
    {
        public IEnumerable<Expression<T>> Expressions { get; }

        public AltExpression(IEnumerable<Expression<T>> expressions)
        {
            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }

            this.Expressions = expressions?.ToList() ?? throw new ArgumentNullException(nameof(expressions));
        }
        
        public override IEnumerable<NondeterministicObject<T>?> Run()
        {
            return this.Expressions.SelectMany(x => x.Run());
        }
    }
}