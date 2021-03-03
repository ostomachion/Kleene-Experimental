using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class PermuteExpression : Expression
    {
        public static readonly PermuteExpression Empty = new PermuteExpression(Enumerable.Empty<Expression>());

        public IEnumerable<Expression> Expressions { get; }
        public PermuteExpression(IEnumerable<Expression> expressions)
        {
            this.Expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));

            if (expressions.Contains(null!))
            {
                throw new ArgumentException("Expressions must not contain null.", nameof(expressions));
            }
        }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            // TODO: This can probably be a lot more efficient.

            if (!this.Expressions.Any())
            {
                yield return null;
            }

            var expressions = this.Expressions.ToList();

            foreach (var expression in expressions)
            {
                var head = expression.Run();
                var test = expressions.Except(EnumerableExt.Yield(expression));
                var tail = new PermuteExpression(test).Run();
                foreach (var value in SequenceExpression.Concat(head, tail))
                {
                    yield return value;
                }
            }
        }
    }
}