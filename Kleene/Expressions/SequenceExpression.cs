using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class SequenceExpression : Expression
    {
        public static readonly SequenceExpression Empty = new SequenceExpression(Enumerable.Empty<Expression>());

        public IEnumerable<Expression> Expressions { get; }
        public SequenceExpression(IEnumerable<Expression> expressions)
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
                return EnumerableExt.Yield<NondeterministicStructure?>(null);
            }

            var head = this.Expressions.First().Run();
            var tail = new SequenceExpression(this.Expressions.Skip(1)).Run();

            return concat(head, tail);

            static IEnumerable<NondeterministicStructure?> concat(IEnumerable<NondeterministicStructure?> head, IEnumerable<NondeterministicStructure?> tail)
            {
                return head.SelectMany(x =>
                    x is null ? tail :
                    x is NamedNondeterministicStructure named ? (IEnumerable<NondeterministicStructure?>)EnumerableExt.Yield(new NamedNondeterministicStructure(named.Name, named.FirstChild, concat(named.NextSibling, tail))) :
                    x is AnyStructure all ? EnumerableExt.Yield(new AnyStructure(concat(all.NextSibling, tail))) :
                    throw new NotImplementedException());
            }
        }
    }
}