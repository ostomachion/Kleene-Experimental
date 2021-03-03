using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public abstract class NondeterministicStructure
    {
        public abstract IEnumerable<Structure> Collapse();
    }
}
