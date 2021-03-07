using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class StructureExpression : Expression<Structure>
    {
        public string Name { get; }
        public Expression<Structure> Value { get; }

        public StructureExpression(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = SequenceExpression<Structure>.Empty;
        }

        public StructureExpression(string name, Expression<Structure> value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            yield return new NamedNondeterministicStructure(this.Name, this.Value.Run());
        }
    }
}