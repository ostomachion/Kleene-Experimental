using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class NondeterministicStructure : NondeterministicObject<Structure>
    {
        public string Name { get; }

        public IEnumerable<NondeterministicObject<ISequencable<Structure>>> Children { get; }

        public NondeterministicStructure(string name, IEnumerable<NondeterministicObject<ISequencable<Structure>>> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public override IEnumerable<Structure> Collapse()
        {
            // TODO:
            throw new NotImplementedException();
        }

        public override IEnumerable<NondeterministicObject<Structure>> Overlap(NondeterministicObject<Structure> other)
        {
            if (other is AnyObject<Structure>)
            {
                yield return this;
            }
            else
            {
                // TODO:
                throw new NotImplementedException();
            }
        }
    }
}
