using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kleene
{
    public class NondeterministicStructure
    {
        public string Name { get; }

        public IEnumerable<NondeterministicStructure?> FirstChild { get; }
        public IEnumerable<NondeterministicStructure?> NextSibling { get; }

        public NondeterministicStructure(string name, IEnumerable<NondeterministicStructure?> firstChild, IEnumerable<NondeterministicStructure?> nextSibling)
        {
            this.Name = name;
            this.FirstChild = firstChild;
            this.NextSibling = nextSibling;
        }
    }
}
