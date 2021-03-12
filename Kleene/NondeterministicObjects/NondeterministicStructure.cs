using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class NondeterministicStructure : NondeterministicObject<Structure>
    {
        public string Name { get; }

        public IEnumerable<NondeterministicObject<ObjectSequence<Structure>>?> Children { get; }

        public NondeterministicStructure(string name, IEnumerable<NondeterministicObject<ObjectSequence<Structure>>?> children)
        {
            this.Name = name;
            this.Children = children;
        }

        public override IEnumerable<Structure> Collapse()
        {
            foreach (var children in this.Children
                .OfType<NondeterministicObject<ObjectSequence<Structure>>>()
                .SelectMany(x => x.Collapse()))
            {
                yield return new Structure(this.Name, children);
            }
        }

        public override IEnumerable<NondeterministicObject<Structure>> Overlap(NondeterministicObject<Structure> other)
        {
            if (other is NondeterministicStructure structure)
            {
                if (this.Name != structure.Name)
                    yield break;
                
                yield return new NondeterministicStructure(this.Name, NondeterministicObject<ObjectSequence<Structure>>.Overlap(this.Children, structure.Children));
            }
            else
            {
                throw new ArgumentException("Argument type is not supported.", nameof(other));
            }
        }
    }
}
