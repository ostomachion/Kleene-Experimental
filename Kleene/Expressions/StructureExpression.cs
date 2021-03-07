using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class StructureExpression : Expression<Structure>
    {
        public string Name { get; }
        public Expression<ISequencable<Structure>> Value { get; }

        public StructureExpression(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = SequenceExpression<Structure>.Empty;
        }

        public StructureExpression(string name, Expression<ISequencable<Structure>> value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<NondeterministicObject<Structure>> Run()
        {
            yield return new NondeterministicStructure(this.Name, this.Value.Run());
        }
    }
}