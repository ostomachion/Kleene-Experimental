using System;
using System.Collections.Generic;

namespace Kleene
{
    public class AnyStructure<T> : NondeterministicObject<T> where T : IRunnable<T>
    {
        public override IEnumerable<T> Collapse() => throw new InvalidOperationException();
    }
}
