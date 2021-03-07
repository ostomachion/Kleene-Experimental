using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class NamedNondeterministicStructure : NondeterministicStructure
    {
        public string Name { get; }

        public IEnumerable<IEnumerable<NondeterministicStructure>> Children { get; }

        public NamedNondeterministicStructure(string name, IEnumerable<IEnumerable<NondeterministicStructure>> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public override IEnumerable<Structure> Collapse()
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}
