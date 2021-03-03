using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public class NamedNondeterministicStructure : NondeterministicStructure
    {
        public string Name { get; }

        public IEnumerable<NondeterministicStructure?> FirstChild { get; }
        public IEnumerable<NondeterministicStructure?> NextSibling { get; }

        public NamedNondeterministicStructure(string name, IEnumerable<NondeterministicStructure?> firstChild, IEnumerable<NondeterministicStructure?> nextSibling)
        {
            this.Name = name;
            this.FirstChild = firstChild;
            this.NextSibling = nextSibling;
        }

        public override IEnumerable<Structure> Collapse()
        {
            var children = this.FirstChild.SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));
            var siblings = this.NextSibling.SelectMany(x => x?.Collapse() ?? EnumerableExt.Yield<Structure?>(null));
            foreach (var sibling in siblings)
            {
                foreach (var child in children)
                {
                    yield return new Structure(this.Name, child, sibling);
                }
            }
        }
    }
}
