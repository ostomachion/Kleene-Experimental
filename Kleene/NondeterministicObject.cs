using System.Collections.Generic;
using System.Linq;

namespace Kleene
{
    public abstract class NondeterministicObject<T> where T : IRunnable<T>
    {
        public abstract IEnumerable<T> Collapse();
    }
}
