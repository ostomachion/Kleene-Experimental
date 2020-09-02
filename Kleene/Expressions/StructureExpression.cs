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
            this.Name = name;
            this.Value = SequenceExpression.Empty;
        }

        public StructureExpression(string name, Expression value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override IEnumerable<NondeterministicStructure?> Run()
        {
            var value = this.Value.Run();
            yield return new NondeterministicStructure(this.Name, this.Value.Run(), EnumerableExt.Yield<NondeterministicStructure?>(null));
        }
    }
}