using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class StructureExpression : Expression
    {
        public string Name { get; }
        public Expression Value { get; }

        public StructureExpression(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = SequenceExpression.Empty;
        }

        public StructureExpression(string name, Expression value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            yield return new NamedNondeterministicStructure(this.Name, this.Value.Run(), EnumerableExt.Yield<NondeterministicStructure?>(null));
        }
    }
}