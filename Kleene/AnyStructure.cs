using System;
using System.Collections.Generic;

namespace Kleene
{
    public class AnyStructure : NondeterministicStructure
    {
        public override IEnumerable<Structure> Collapse() => throw new InvalidOperationException();
    }
}
