using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class NondeterministicObjectDictionary<TKey, TValue> : NondeterministicObject<ObjectDictionary<TKey, TValue>> where TKey : IRunnable<TKey> where TValue : IRunnable<TValue>
    {
        public NondeterministicObjectDictionary()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ObjectDictionary<TKey, TValue>> Collapse()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<NondeterministicObject<ObjectDictionary<TKey, TValue>>> Overlap(NondeterministicObject<ObjectDictionary<TKey, TValue>> other)
        {
            throw new NotImplementedException();
        }
    }
}
