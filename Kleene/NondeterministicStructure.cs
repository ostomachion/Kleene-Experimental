using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class NondeterministicStructure : NondeterministicObject<Structure>
    {
        public string Name { get; }

        public IEnumerable<IEnumerable<NondeterministicObject<Structure>>> Children { get; }

        public NondeterministicStructure(string name, IEnumerable<IEnumerable<NondeterministicObject<Structure>>> children)
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
