using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class CaptureExpression : Expression
    {
        public Expression Expression { get; }
        public string Name { get; }

        public CaptureExpression(Expression expression, string name)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override IEnumerable<Result> Run(IEnumerable<Structure> input, int index)
        {
            foreach (var result in this.Expression.Run(input, index))
            {
                yield return new Result(input, index, result.Length, this, new [] { result });
            }
        }
    }
}