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
            this.Expressions = expressions;
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