using System;
using System.Collections.Generic;

namespace Kleene
{
    public class AnyStructure : NondeterministicStructure
    {
        public IEnumerable<NondeterministicStructure?> NextSibling { get; }

        public AnyStructure(IEnumerable<NondeterministicStructure?> nextSibling)
        {
            NextSibling = nextSibling;
        }

        public override IEnumerable<Structure> Collapse() => throw new InvalidOperationException();
    }
}
